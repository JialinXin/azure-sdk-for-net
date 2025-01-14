// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.ResourceManager;

namespace Azure.ResourceManager.ManagedNetworkFabric
{
    internal class NetworkToNetworkInterconnectOperationSource : IOperationSource<NetworkToNetworkInterconnectResource>
    {
        private readonly ArmClient _client;

        internal NetworkToNetworkInterconnectOperationSource(ArmClient client)
        {
            _client = client;
        }

        NetworkToNetworkInterconnectResource IOperationSource<NetworkToNetworkInterconnectResource>.CreateResult(Response response, CancellationToken cancellationToken)
        {
            using var document = JsonDocument.Parse(response.ContentStream);
            var data = NetworkToNetworkInterconnectData.DeserializeNetworkToNetworkInterconnectData(document.RootElement);
            return new NetworkToNetworkInterconnectResource(_client, data);
        }

        async ValueTask<NetworkToNetworkInterconnectResource> IOperationSource<NetworkToNetworkInterconnectResource>.CreateResultAsync(Response response, CancellationToken cancellationToken)
        {
            using var document = await JsonDocument.ParseAsync(response.ContentStream, default, cancellationToken).ConfigureAwait(false);
            var data = NetworkToNetworkInterconnectData.DeserializeNetworkToNetworkInterconnectData(document.RootElement);
            return new NetworkToNetworkInterconnectResource(_client, data);
        }
    }
}
