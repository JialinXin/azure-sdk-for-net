// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Client certificate info.
    /// </summary>
    public sealed class ClientCertificateInfo
    {
        /// <summary>
        /// Certificate thumbprint.
        /// </summary>
        public string Thumbprint { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="thumbprint"></param>
        internal ClientCertificateInfo(string thumbprint)
        {
            Thumbprint = thumbprint;
        }
    }
}
