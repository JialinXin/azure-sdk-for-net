// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Options when using Web PubSub service.
    /// </summary>
    public class WebPubSubValidationOptions
    {
        internal Dictionary<string, string> _hostKeyMappings = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="connectionStrings"></param>
        public WebPubSubValidationOptions(params string[] connectionStrings)
        {
            foreach (var item in connectionStrings)
            {
                (Uri host, AzureKeyCredential credential) = WebPubSubServiceClient.ParseConnectionString(item);
                _hostKeyMappings.Add(host.Host, credential.Key);
            }
        }
    }
}
