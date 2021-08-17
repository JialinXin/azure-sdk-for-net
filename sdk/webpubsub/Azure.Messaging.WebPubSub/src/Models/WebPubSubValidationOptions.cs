// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.Messaging.WebPubSub
{
    /// <summary>
    /// Validation options when using Web PubSub service.
    /// </summary>
    public class WebPubSubValidationOptions
    {
        private readonly Dictionary<string, string> _hostKeyMappings = new(StringComparer.OrdinalIgnoreCase);

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

        internal bool ContainsHost()
        {
            return _hostKeyMappings.Count > 0;
        }

        internal bool ContainsHost(string host)
        {
            return _hostKeyMappings.ContainsKey(host);
        }

        internal bool TryGetKey(string host, out string accessKey)
        {
            if (_hostKeyMappings.TryGetValue(host, out accessKey))
            {
                return true;
            }
            return false;
        }
    }
}
