// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Event type.
    /// </summary>
    [CodeGenModel("WebPubSubEventType")]
    public enum WebPubSubEventType
    {
        /// <summary>
        /// system event, including connect, connected, disconnected.
        /// </summary>
        System,
        /// <summary>
        /// user event.
        /// </summary>
        User
    }
}
