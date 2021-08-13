// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Hub methods.
    /// </summary>
    public abstract class ServiceHub : IDisposable
    {
        /// <summary>
        /// Connect event method.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>ConnectResponse or ErrorResponse.</returns>
        public virtual ServiceResponse Connect(ConnectEventRequest request)
        {
            return new ConnectResponse
            {
                UserId = request.ConnectionContext.UserId
            };
        }

        /// <summary>
        /// Message event method.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>MessageResponse or ErrorResponse.</returns>
        public virtual ServiceResponse Message(MessageEventRequest request)
        {
            return new MessageResponse();
        }

        /// <summary>
        /// Connected event method.
        /// </summary>
        /// <param name="request"></param>
        public virtual void Connected(ConnectedEventRequest request)
        { }

        /// <summary>
        /// Disconnected event method.
        /// </summary>
        /// <param name="request"></param>
        public virtual void Disconnected(DisconnectedEventRequest request)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        public ServiceHub()
        { }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
