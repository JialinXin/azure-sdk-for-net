// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// service handler.
    /// </summary>
    public interface IServiceRequestHandler
    {
        /// <summary>
        /// handle request.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hub"></param>
        /// <returns></returns>
        Task HandleRequest<THub>(HttpContext context, THub hub) where THub : ServiceHub;
    }
}
