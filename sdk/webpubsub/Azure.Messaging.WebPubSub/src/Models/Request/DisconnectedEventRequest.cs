// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Disconnected event request.
    /// </summary>
    public sealed class DisconnectedEventRequest : ServiceRequest
    {
        /// <summary>
        /// Reason.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// Name of the request.
        /// </summary>
        public override string Name => nameof(DisconnectedEventRequest);

        internal DisconnectedEventRequest(ConnectionContext connectionContext, string reason)
            : base(connectionContext)
        {
            Reason = reason;
        }
    }
}
