// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Runtime.Serialization;
using Azure.Core;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Response Error Code.
    /// </summary>
    [CodeGenModel("WebPubSubErrorCode")]
    public enum WebPubSubErrorCode
    {
        /// <summary>
        /// Unauthorized.
        /// </summary>
        Unauthorized,
        /// <summary>
        /// User Error.
        /// </summary>
        UserError,
        /// <summary>
        /// Server Error.
        /// </summary>
        ServerError
    }
}
