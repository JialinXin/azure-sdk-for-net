// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Azure.Messaging.WebPubSub
{
    internal class ServiceRequestHandler : IServiceRequestHandler
    {
        private readonly WebPubSubValidationOptions _options;

        public ServiceRequestHandler(WebPubSubValidationOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Handle requests.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        public async Task HandleRequest<THub>(HttpContext context, THub instance) where THub: ServiceHub
        {
            HttpRequest request = context.Request;

            if (context == null)
            {
                return;
            }

            // abuse validation.
            if (request.IsAbuseCheck(out ValidationRequest validationRequest))
            {
                if (_options != null)
                {
                    foreach (var item in validationRequest.RequestHosts)
                    {
                        if (_options._hostKeyMappings.ContainsKey(item))
                        {
                            break;
                        }
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Abuse Protection validation failed.").ConfigureAwait(false);
                        return;
                    }
                }
                context.Response.StatusCode = 200;
                context.Response.Headers.Add(Constants.Headers.WebHookAllowedOrigin, Constants.DefaultAllowedHost);
                return;
            }

            if (!request.TryParseRequest(out ConnectionContext connectionContext))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Unknown upstream request.").ConfigureAwait(false);
                return;
            }

            // Signature check
            if (_options != null && _options._hostKeyMappings.Count > 0)
            {
                if (!_options._hostKeyMappings.TryGetValue(connectionContext.Origin, out var accessKey)
                    || !RequestHelper.ValidateSignature(connectionContext.ConnectionId, connectionContext.Signature, accessKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Signature validation failed.").ConfigureAwait(false);
                    return;
                }
            }

            RequestType requestType = RequestHelper.GetRequestType(connectionContext.EventType, connectionContext.EventName);
            //var hubInstance = context.RequestServices.GetService(typeof(THub));
            switch (requestType)
            {
                case RequestType.Connect:
                    {
                        var content = await new StreamReader(request.Body).ReadToEndAsync().ConfigureAwait(false);
                        var eventRequest = JsonSerializer.Deserialize<ConnectEventRequest>(content);
                        eventRequest.ConnectionContext = connectionContext;
                        var response = instance.Connect(eventRequest);
                        //var response = InvokeMethod<THub>("Connect", eventRequest, hubInstance);
                        if (response is ErrorResponse error)
                        {
                            context.Response.StatusCode = RequestHelper.GetStatusCode(error.Code);
                            await context.Response.WriteAsync(error.ErrorMessage).ConfigureAwait(false);
                            return;
                        }
                        else if (response is ConnectResponse connectResponse)
                        {
                            context.Response.StatusCode = 200;
                            await context.Response.WriteAsync(JsonSerializer.Serialize(connectResponse)).ConfigureAwait(false);
                            return;
                        }
                    }
                    break;
                case RequestType.User:
                    {
                        using var ms = new MemoryStream();
                        await request.Body.CopyToAsync(ms).ConfigureAwait(false);
                        var message = BinaryData.FromBytes(ms.ToArray());
                        if (!RequestHelper.ValidateMediaType(MediaTypeHeaderValue.Parse(request.ContentType).MediaType, out var dataType))
                        {
                            context.Response.StatusCode = 400;
                            await context.Response.WriteAsync($"ContentType is not supported: {request.ContentType}").ConfigureAwait(false);
                            return;
                        }
                        var eventRequest = new MessageEventRequest(connectionContext, message, dataType);
                        var response = instance.Message(eventRequest);
                        //var response = InvokeMethod<THub>("Message", eventRequest, hubInstance);
                        if (response is ErrorResponse error)
                        {
                            context.Response.StatusCode = RequestHelper.GetStatusCode(error.Code);
                            await context.Response.WriteAsync(error.ErrorMessage).ConfigureAwait(false);
                            return;
                        }
                        else if (response is MessageResponse msgResponse)
                        {
                            context.Response.StatusCode = 200;
                            await context.Response.WriteAsync(JsonSerializer.Serialize(msgResponse)).ConfigureAwait(false);
                            return;
                        }
                    }
                    break;
                case RequestType.Connected:
                    {
                        instance.Connected(new ConnectedEventRequest(connectionContext));
                        //InvokeMethod<THub>(nameof(ServiceHub.Connected), (new ConnectedEventRequest(connectionContext)), hubInstance);
                    }
                    break;
                case RequestType.Disconnected:
                    {
                        var content = await new StreamReader(request.Body).ReadToEndAsync().ConfigureAwait(false);
                        var eventRequest = JsonSerializer.Deserialize<DisconnectedEventRequest>(content);
                        eventRequest.ConnectionContext = connectionContext;
                        instance.Disconnected(eventRequest);
                        //InvokeMethod<THub>(nameof(ServiceHub.Disconnected), eventRequest, hubInstance);
                    }
                    break;
                default:
                    break;
            }
        }

        //private static ServiceResponse InvokeMethod<THub>(string methodName, ServiceRequest request, object instance)
        //{
        //    var method = typeof(THub).GetMethod(methodName);
        //    if (instance == null)
        //    {
        //        var constructor = typeof(THub).GetConstructor(Type.EmptyTypes);
        //        instance = constructor.Invoke(Array.Empty<object>());
        //    }
        //    return method.Invoke(instance, new[] { request }) as ServiceResponse;
        //}
    }
}
