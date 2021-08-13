// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Messaging.WebPubSub;
using System;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Response for message events.
    /// </summary>
    public class MessageResponse : ServiceResponse
    {
        /// <summary>
        /// Message.
        /// </summary>
        public BinaryData Message { get; set; }

        /// <summary>
        /// Message data type.
        /// </summary>
        public MessageDataType DataType { get; set; } = MessageDataType.Text;
    }
}
