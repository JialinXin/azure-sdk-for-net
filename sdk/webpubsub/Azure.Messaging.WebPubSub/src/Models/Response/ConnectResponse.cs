// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Response for connect event.
    /// </summary>
    public class ConnectResponse : ServiceResponse
    {
        /// <summary>
        /// UserId.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Groups.
        /// </summary>
        public string[] Groups { get; set; }

        /// <summary>
        /// Subprotocol.
        /// </summary>
        public string Subprotocol { get; set; }

        /// <summary>
        /// User roles.
        /// </summary>
        public string[] Roles { get; set; }
    }
}
