// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Connect event request.
    /// </summary>
    public sealed class ConnectEventRequest : ServiceRequest
    {
        /// <summary>
        /// User Claims.
        /// </summary>
        [JsonPropertyName("claim")]
#pragma warning disable CA2227 // Collection properties should be read only
        public IDictionary<string, string[]> Claims { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Query.
        /// </summary>
        [JsonPropertyName("query")]
#pragma warning disable CA2227 // Collection properties should be read only
        public IDictionary<string, string[]> Query { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Supported subprotocols.
        /// </summary>
        [JsonPropertyName("subprotocols")]
        public string[] Subprotocols { get; set; }

        /// <summary>
        /// Client certificates.
        /// </summary>
        [JsonPropertyName("clientCertificates")]
        public ClientCertificateInfo[] ClientCertificates { get; set; }

        /// <summary>
        /// string name of the request.
        /// </summary>
        public override string Name => nameof(ConnectEventRequest);

        /// <summary>
        /// json deserialize.
        /// </summary>
        public ConnectEventRequest()
            : base(null)
        { }

        internal ConnectEventRequest(ConnectionContext connectionContext, IDictionary<string, string[]> claims, IDictionary<string, string[]> query, string[] subprotocols, ClientCertificateInfo[] clientCertificateInfos)
            : base(connectionContext)
        {
            Claims = claims;
            Query = query;
            Subprotocols = subprotocols;
            ClientCertificates = clientCertificateInfos;
        }
    }
}
