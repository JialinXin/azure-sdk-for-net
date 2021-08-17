// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Help methods to parse upstream requests.
    /// </summary>
    public static class RequestHelper
    {
        /// <summary>
        /// Parse upstream request headers following CloudEvents.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static bool TryParseCloudEvents(this HttpRequest request, out ConnectionContext connection)
        {
            // ConnectionId is required in upstream request, and method is POST.
            if (!request.Headers.ContainsKey(Constants.Headers.CloudEvents.ConnectionId)
                || !request.Method.Equals("post", StringComparison.OrdinalIgnoreCase))
            {
                connection = null;
                return false;
            }

            connection = new ConnectionContext();
            try
            {
                connection.ConnectionId = GetHeaderValueOrDefault(request.Headers, Constants.Headers.CloudEvents.ConnectionId);
                connection.Hub = GetHeaderValueOrDefault(request.Headers, Constants.Headers.CloudEvents.Hub);
                connection.EventType = GetEventType(GetHeaderValueOrDefault(request.Headers, Constants.Headers.CloudEvents.Type));
                connection.EventName = GetHeaderValueOrDefault(request.Headers, Constants.Headers.CloudEvents.EventName);
                connection.Signature = GetHeaderValueOrDefault(request.Headers, Constants.Headers.CloudEvents.Signature);
                connection.Origin = GetHeaderValueOrDefault(request.Headers, Constants.Headers.WebHookRequestOrigin);
                connection.Headers = request.Headers.ToDictionary(x => x.Key, v => new StringValues(v.Value.ToArray()), StringComparer.OrdinalIgnoreCase);

                // UserId is optional, e.g. connect
                if (request.Headers.ContainsKey(Constants.Headers.CloudEvents.UserId))
                {
                    connection.UserId = GetHeaderValueOrDefault(request.Headers, Constants.Headers.CloudEvents.UserId);
                }

                // connection states.
                if (request.Headers.ContainsKey(Constants.Headers.CloudEvents.State))
                {
                    connection.States = DecodeConnectionState(GetHeaderValueOrDefault(request.Headers, Constants.Headers.CloudEvents.State));
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether request is validation request of abuse protection.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validationRequest"></param>
        /// <returns></returns>
        public static bool IsValidationRequest(this HttpRequest request, out ValidationRequest validationRequest)
        {
            if (request.Method.Equals("options", StringComparison.OrdinalIgnoreCase))
            {
                request.Headers.TryGetValue(Constants.Headers.WebHookRequestOrigin, out StringValues requestHosts);
                if (requestHosts.Any())
                {
                    validationRequest = new ValidationRequest(true, requestHosts.ToList());
                    return true;
                }
            }
            validationRequest = null;
            return false;
        }

        /// <summary>
        /// Validate request signature by connection-id and service key.
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="signature"></param>
        /// <param name="accessKey"></param>
        /// <returns></returns>
        public static bool ValidateSignature(string connectionId, string signature, string accessKey)
        {
            IReadOnlyList<string> signatures = GetSignatureList(signature);
            if (signatures == null)
            {
                return false;
            }
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(accessKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(connectionId));
            var hash = "sha256=" + BitConverter.ToString(hashBytes).Replace("-", "");
            if (signatures.Contains(hash, StringComparer.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Parse request to different types.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<ServiceRequest> ToServiceRequest(this HttpRequest request, ConnectionContext context = null)
        {
            if (context != null || request.TryParseCloudEvents(out context))
            {
                var requestType = GetRequestType(context.EventType, context.EventName);
                switch (requestType)
                {
                    case RequestType.Connect:
                        {
                            var content = await new StreamReader(request.Body).ReadToEndAsync().ConfigureAwait(false);
                            var eventRequest = JsonSerializer.Deserialize<ConnectEventRequest>(content);
                            eventRequest.ConnectionContext = context;
                            return eventRequest;
                        }
                    case RequestType.User:
                        {
                            using var ms = new MemoryStream();
                            await request.Body.CopyToAsync(ms).ConfigureAwait(false);
                            var message = BinaryData.FromBytes(ms.ToArray());
                            if (!ValidateMediaType(MediaTypeHeaderValue.Parse(request.ContentType).MediaType, out var dataType))
                            {
                                return new InvalidRequest($"ContentType is not supported: {request.ContentType}");
                            }
                            return new MessageEventRequest(context, message, dataType);
                        }
                    case RequestType.Connected:
                        {
                            return new ConnectedEventRequest(context);
                        }
                    case RequestType.Disconnected:
                        {
                            var content = await new StreamReader(request.Body).ReadToEndAsync().ConfigureAwait(false);
                            var eventRequest = JsonSerializer.Deserialize<DisconnectedEventRequest>(content);
                            eventRequest.ConnectionContext = context;
                            return eventRequest;
                        }
                    default:
                        break;
                }
            }
            return new InvalidRequest($"Unknown upstream request, type: {context.EventType}, name: {context.EventName}");
        }

        /// <summary>
        /// Convert WebPubSubErrorCode to status code.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static int ToStatusCode(this WebPubSubErrorCode errorCode) =>
            errorCode switch
            {
                WebPubSubErrorCode.UserError => 400,
                WebPubSubErrorCode.Unauthorized => 401,
                // default and server error returns 500
                _ => 500
            };

        /// <summary>
        /// Convert MessageDataType to request ContentType.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static string ToContentType(this MessageDataType dataType) =>
            dataType switch
            {
                MessageDataType.Text => Constants.ContentTypes.PlainTextContentType,
                MessageDataType.Json => Constants.ContentTypes.JsonContentType,
                _ => Constants.ContentTypes.BinaryContentType
            };

        /// <summary>
        /// Convert request MediaType to MessageDataType.
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static MessageDataType GetDataType(string mediaType) =>
            mediaType.ToLowerInvariant() switch
            {
                Constants.ContentTypes.BinaryContentType => MessageDataType.Binary,
                Constants.ContentTypes.JsonContentType => MessageDataType.Json,
                Constants.ContentTypes.PlainTextContentType => MessageDataType.Text,
                _ => throw new ArgumentException($"Not supported content type: {mediaType}")
            };

        /// <summary>
        /// Decode connection state.
        /// </summary>
        /// <param name="connectionStates"></param>
        /// <returns></returns>
        public static Dictionary<string, object> DecodeConnectionState(string connectionStates)
        {
            if (!string.IsNullOrEmpty(connectionStates))
            {
                var states = new Dictionary<string, object>();
                var parsedStates = Encoding.UTF8.GetString(Convert.FromBase64String(connectionStates));
                var statesObj = JsonDocument.Parse(parsedStates);
                foreach (var item in statesObj.RootElement.EnumerateObject())
                {
                    // Use ToString() to set pure value without ValueKind to compatible with Newtonsoft.Json.
                    states.Add(item.Name, item.Value.ToString());
                }
                return states;
            }
            return null;
        }

        /// <summary>
        /// Merge connection state.
        /// </summary>
        /// <param name="existValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static Dictionary<string, object> UpdateConnectionState(Dictionary<string, object> existValue, Dictionary<string, object> newValue)
        {
            // clear states.
            if (newValue == null)
            {
                return null;
            }
            // empty is no change.
            if (newValue.Count < 1)
            {
                return existValue;
            }

            // updates based on exist value.
            foreach (var item in newValue)
            {
                if (existValue.ContainsKey(item.Key))
                {
                    existValue[item.Key] = item.Value;
                }
                else
                {
                    existValue.Add(item.Key, item.Value);
                }
            }
            return existValue;
        }

        /// <summary>
        /// Convert connection state to request header value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHeaderStates(this Dictionary<string, object> value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value)));
        }

        internal static RequestType GetRequestType(WebPubSubEventType eventType, string eventName)
        {
            if (eventType == WebPubSubEventType.User)
            {
                return RequestType.User;
            }
            if (eventName.Equals(Constants.Events.ConnectEvent, StringComparison.OrdinalIgnoreCase))
            {
                return RequestType.Connect;
            }
            if (eventName.Equals(Constants.Events.DisconnectedEvent, StringComparison.OrdinalIgnoreCase))
            {
                return RequestType.Disconnected;
            }
            if (eventName.Equals(Constants.Events.ConnectedEvent, StringComparison.OrdinalIgnoreCase))
            {
                return RequestType.Connected;
            }
            return RequestType.Ignored;
        }

        internal static bool ValidateMediaType(string mediaType, out MessageDataType dataType)
        {
            try
            {
                dataType = GetDataType(mediaType);
                return true;
            }
            catch (Exception)
            {
                dataType = MessageDataType.Binary;
                return false;
            }
        }

        private static IReadOnlyList<string> GetSignatureList(string signatures)
        {
            if (string.IsNullOrEmpty(signatures))
            {
                return default;
            }

            return signatures.Split(Constants.HeaderSeparator, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string GetHeaderValueOrDefault(IHeaderDictionary header, string key)
        {
            return header.TryGetValue(key, out StringValues value) ? value[0] : null;
        }

        private static WebPubSubEventType GetEventType(string ceType)
        {
            return ceType.StartsWith(Constants.Headers.CloudEvents.TypeSystemPrefix, StringComparison.OrdinalIgnoreCase) ?
                WebPubSubEventType.System :
                WebPubSubEventType.User;
        }
    }
}
