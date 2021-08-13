// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Primitives;

namespace Azure.Messaging.WebPubSub.Extensions
{
    /// <summary>
    /// Extensions to parse HttpRequest to WebPubSubRequest.
    /// </summary>
    public static class HttpContextExtensions
    {
        //private const string WebHookRequestOrigin = "WebHook-Request-Origin";
        //private const string WebHookAllowedOrigin = "WebHook-Allowed-Origin";
        //
        //private const string Prefix = "ce-";
        //private const string Signature = Prefix + "signature";
        //private const string Hub = Prefix + "hub";
        //private const string ConnectionId = Prefix + "connectionId";
        ////private const string Id = Prefix + "id";
        ////private const string Time = Prefix + "time";
        ////private const string SpecVersion = Prefix + "specversion";
        //private const string Type = Prefix + "type";
        //private const string EventName = Prefix + "eventName";
        //private const string UserId = Prefix + "userId";
        ////private const string State = Prefix + "connectionState";
        //
        //private const string TypeSystemPrefix = "azure.webpubsub.sys.";
        //private const string TypeUserPrefix = "azure.webpubsub.user.";

        ///// <summary>
        ///// Method to parse WebPubSubRequest
        ///// </summary>
        ///// <param name="context"></param>
        //public static WebPubSubRequest ToWebPubSubRequest(this HttpContext context)
        //{
        //    if (context == null)
        //    {
        //        return new WebPubSubRequest(null, new InvalidRequest(HttpStatusCode.BadRequest), HttpStatusCode.BadRequest);
        //    }
        //
        //    if (context.IsAbuseCheck(out var validationRequest))
        //    {
        //        return new WebPubSubRequest(null, validationRequest, ValidationResponse());
        //    }
        //
        //    if (!context.TryParseRequest(out var connectionContext))
        //    {
        //        return new WebPubSubRequest(null, new InvalidRequest(HttpStatusCode.BadRequest, "Invalid request that missing required fields."), HttpStatusCode.BadRequest);
        //    }
        //
        //    // Signature checks
        //    var client = context.RequestServices.GetRequiredService<WebPubSubServiceClient>();
        //    // TODO: fix.
        //    return new WebPubSubRequest(connectionContext, null);
        //}
        //
        //private static bool IsAbuseCheck(this HttpContext context, out ValidationRequest validationRequest)
        //{
        //    if (context.Request.Method.Equals("options", StringComparison.OrdinalIgnoreCase))
        //    {
        //        context.Request.Headers.TryGetValue(WebHookRequestOrigin, out var requestHosts);
        //        if (requestHosts.Any())
        //        {
        //            validationRequest = new ValidationRequest(true, requestHosts);
        //            return true;
        //        }
        //    }
        //    validationRequest = null;
        //    return false;
        //}
        //
        //private static HttpResponseMessage ValidationResponse()
        //{
        //    var response = new HttpResponseMessage();
        //    response.Headers.Add(WebHookAllowedOrigin, "*");
        //    return response;
        //}
        //
        //private static bool TryParseRequest(this HttpContext context, out ConnectionContext connection)
        //{
        //    // ConnectionId is required in upstream request, and method is POST.
        //    if (!context.Request.Headers.ContainsKey(ConnectionId)
        //        || !context.Request.Method.Equals("post", StringComparison.OrdinalIgnoreCase))
        //    {
        //        connection = null;
        //        return false;
        //    }
        //
        //    connection = new ConnectionContext();
        //    try
        //    {
        //        connection.ConnectionId = GetHeaderValueOrDefault(context.Request.Headers, ConnectionId);
        //        connection.Hub = GetHeaderValueOrDefault(context.Request.Headers, Hub);
        //        connection.EventType = GetEventType(GetHeaderValueOrDefault(context.Request.Headers, Type));
        //        connection.EventName = GetHeaderValueOrDefault(context.Request.Headers, EventName);
        //        connection.Signature = GetHeaderValueOrDefault(context.Request.Headers, Signature);
        //        connection.Headers = context.Request.Headers.ToDictionary(x => x.Key, v => new StringValues(v.Value.ToArray()), StringComparer.OrdinalIgnoreCase);
        //
        //        // UserId is optional, e.g. connect
        //        if (context.Request.Headers.ContainsKey(UserId))
        //        {
        //            connection.UserId = GetHeaderValueOrDefault(context.Request.Headers, UserId);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //
        //    return true;
        //}
        //
        //private static string GetHeaderValueOrDefault(IHeaderDictionary header, string key)
        //{
        //    return header.TryGetValue(key, out var value) ? value[0] : null;
        //}
        //
        //private static WebPubSubEventType GetEventType(string ceType)
        //{
        //    return ceType.StartsWith(TypeSystemPrefix, StringComparison.OrdinalIgnoreCase) ?
        //        WebPubSubEventType.System :
        //        WebPubSubEventType.User;
        //}
        //
        //private static RequestType GetRequestType(WebPubSubEventType eventType, string eventName)
        //{
        //    if (eventType == WebPubSubEventType.User)
        //    {
        //        return RequestType.User;
        //    }
        //    if (eventName.Equals("connect", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return RequestType.Connect;
        //    }
        //    if (eventName.Equals("disconnected", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return RequestType.Disconnected;
        //    }
        //    if (eventName.Equals("connected", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return RequestType.Connected;
        //    }
        //    return RequestType.Ignored;
        //}
    }
}
