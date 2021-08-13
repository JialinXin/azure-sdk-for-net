// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// ServiceRequest for connected event.
    /// </summary>
    public class ConnectedEventRequest : ServiceRequest
    {
        /// <summary>
        /// string value of the request.
        /// </summary>
        public override string Name => nameof(ConnectEventRequest);

        /// <summary>
        /// Constrator.
        /// </summary>
        internal ConnectedEventRequest(ConnectionContext connectionContext) : base(connectionContext)
        {
        }
    }
}
