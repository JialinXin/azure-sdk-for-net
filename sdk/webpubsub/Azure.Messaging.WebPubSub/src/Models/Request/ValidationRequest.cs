// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.Messaging.WebPubSub
{
    internal sealed class ValidationRequest : ServiceRequest
    {
        public bool Valid { get; }
        public List<string> RequestHosts { get; }

        public override string Name => nameof(ValidationRequest);

        public ValidationRequest(bool valid, List<string> requestHosts)
            :base(null)
        {
            Valid = valid;
            RequestHosts = requestHosts;
        }
    }
}
