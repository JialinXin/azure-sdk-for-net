// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Message data type.
    /// </summary>
    [CodeGenModel("MessageDataType")]
    public enum MessageDataType
    {
        /// <summary>
        /// binary of application/octet-stream.
        /// </summary>
        Binary,
        /// <summary>
        /// json of application/json.
        /// </summary>
        Json,
        /// <summary>
        /// text of text/plain.
        /// </summary>
        Text
    }
}
