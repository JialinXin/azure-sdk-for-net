// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Azure.Messaging.WebPubSub
{
    internal static class RequestHelper
    {
        public static bool TryParseRequest(this HttpRequest request, out ConnectionContext connection)
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
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool IsAbuseCheck(this HttpRequest request, out ValidationRequest validationRequest)
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

        public static RequestType GetRequestType(WebPubSubEventType eventType, string eventName)
        {
            if (eventType == WebPubSubEventType.User)
            {
                return RequestType.User;
            }
            if (eventName.Equals("connect", StringComparison.OrdinalIgnoreCase))
            {
                return RequestType.Connect;
            }
            if (eventName.Equals("disconnected", StringComparison.OrdinalIgnoreCase))
            {
                return RequestType.Disconnected;
            }
            if (eventName.Equals("connected", StringComparison.OrdinalIgnoreCase))
            {
                return RequestType.Connected;
            }
            return RequestType.Ignored;
        }

        public static int GetStatusCode(WebPubSubErrorCode errorCode) =>
            errorCode switch
            {
                WebPubSubErrorCode.UserError => 400,
                WebPubSubErrorCode.Unauthorized => 401,
                // default and server error returns 500
                _ => 500
            };

        public static MessageDataType GetDataType(string mediaType) =>
            mediaType.ToLowerInvariant() switch
            {
                Constants.ContentTypes.BinaryContentType => MessageDataType.Binary,
                Constants.ContentTypes.JsonContentType => MessageDataType.Json,
                Constants.ContentTypes.PlainTextContentType => MessageDataType.Text,
                _ => throw new ArgumentException($"Not supported content type: {mediaType}")
            };

        public static bool ValidateMediaType(string mediaType, out MessageDataType dataType)
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
