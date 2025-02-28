// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.ManagedNetworkFabric;
using Azure.ResourceManager.ManagedNetworkFabric.Models;
using Azure.ResourceManager.Resources;

namespace Azure.ResourceManager.ManagedNetworkFabric.Samples
{
    public partial class Sample_NetworkFabricIPExtendedCommunityResource
    {
        // IpExtendedCommunities_Get_MaximumSet_Gen
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("Only verifying that the sample builds")]
        public async Task Get_IpExtendedCommunitiesGetMaximumSetGen()
        {
            // Generated from example definition: specification/managednetworkfabric/resource-manager/Microsoft.ManagedNetworkFabric/stable/2023-06-15/examples/IpExtendedCommunities_Get_MaximumSet_Gen.json
            // this example is just showing the usage of "IpExtendedCommunities_Get" operation, for the dependent resources, they will have to be created separately.

            // get your azure access token, for more details of how Azure SDK get your access token, please refer to https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication?tabs=command-line
            TokenCredential cred = new DefaultAzureCredential();
            // authenticate your client
            ArmClient client = new ArmClient(cred);

            // this example assumes you already have this NetworkFabricIPExtendedCommunityResource created on azure
            // for more information of creating NetworkFabricIPExtendedCommunityResource, please refer to the document of NetworkFabricIPExtendedCommunityResource
            string subscriptionId = "1234ABCD-0A1B-1234-5678-123456ABCDEF";
            string resourceGroupName = "example-rg";
            string ipExtendedCommunityName = "example-ipExtendedCommunity";
            ResourceIdentifier networkFabricIPExtendedCommunityResourceId = NetworkFabricIPExtendedCommunityResource.CreateResourceIdentifier(subscriptionId, resourceGroupName, ipExtendedCommunityName);
            NetworkFabricIPExtendedCommunityResource networkFabricIPExtendedCommunity = client.GetNetworkFabricIPExtendedCommunityResource(networkFabricIPExtendedCommunityResourceId);

            // invoke the operation
            NetworkFabricIPExtendedCommunityResource result = await networkFabricIPExtendedCommunity.GetAsync();

            // the variable result is a resource, you could call other operations on this instance as well
            // but just for demo, we get its data from this resource instance
            NetworkFabricIPExtendedCommunityData resourceData = result.Data;
            // for demo we just print out the id
            Console.WriteLine($"Succeeded on id: {resourceData.Id}");
        }

        // IpExtendedCommunities_Update_MaximumSet_Gen
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("Only verifying that the sample builds")]
        public async Task Update_IpExtendedCommunitiesUpdateMaximumSetGen()
        {
            // Generated from example definition: specification/managednetworkfabric/resource-manager/Microsoft.ManagedNetworkFabric/stable/2023-06-15/examples/IpExtendedCommunities_Update_MaximumSet_Gen.json
            // this example is just showing the usage of "IpExtendedCommunities_Update" operation, for the dependent resources, they will have to be created separately.

            // get your azure access token, for more details of how Azure SDK get your access token, please refer to https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication?tabs=command-line
            TokenCredential cred = new DefaultAzureCredential();
            // authenticate your client
            ArmClient client = new ArmClient(cred);

            // this example assumes you already have this NetworkFabricIPExtendedCommunityResource created on azure
            // for more information of creating NetworkFabricIPExtendedCommunityResource, please refer to the document of NetworkFabricIPExtendedCommunityResource
            string subscriptionId = "1234ABCD-0A1B-1234-5678-123456ABCDEF";
            string resourceGroupName = "example-rg";
            string ipExtendedCommunityName = "example-ipExtendedCommunity";
            ResourceIdentifier networkFabricIPExtendedCommunityResourceId = NetworkFabricIPExtendedCommunityResource.CreateResourceIdentifier(subscriptionId, resourceGroupName, ipExtendedCommunityName);
            NetworkFabricIPExtendedCommunityResource networkFabricIPExtendedCommunity = client.GetNetworkFabricIPExtendedCommunityResource(networkFabricIPExtendedCommunityResourceId);

            // invoke the operation
            NetworkFabricIPExtendedCommunityPatch patch = new NetworkFabricIPExtendedCommunityPatch()
            {
                IPExtendedCommunityRules =
{
new IPExtendedCommunityRule(CommunityActionType.Permit,4155123341,new string[]
{
"1234:2345"
})
},
                Tags =
{
["keyID"] = "KeyValue",
},
            };
            ArmOperation<NetworkFabricIPExtendedCommunityResource> lro = await networkFabricIPExtendedCommunity.UpdateAsync(WaitUntil.Completed, patch);
            NetworkFabricIPExtendedCommunityResource result = lro.Value;

            // the variable result is a resource, you could call other operations on this instance as well
            // but just for demo, we get its data from this resource instance
            NetworkFabricIPExtendedCommunityData resourceData = result.Data;
            // for demo we just print out the id
            Console.WriteLine($"Succeeded on id: {resourceData.Id}");
        }

        // IpExtendedCommunities_Delete_MaximumSet_Gen
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("Only verifying that the sample builds")]
        public async Task Delete_IpExtendedCommunitiesDeleteMaximumSetGen()
        {
            // Generated from example definition: specification/managednetworkfabric/resource-manager/Microsoft.ManagedNetworkFabric/stable/2023-06-15/examples/IpExtendedCommunities_Delete_MaximumSet_Gen.json
            // this example is just showing the usage of "IpExtendedCommunities_Delete" operation, for the dependent resources, they will have to be created separately.

            // get your azure access token, for more details of how Azure SDK get your access token, please refer to https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication?tabs=command-line
            TokenCredential cred = new DefaultAzureCredential();
            // authenticate your client
            ArmClient client = new ArmClient(cred);

            // this example assumes you already have this NetworkFabricIPExtendedCommunityResource created on azure
            // for more information of creating NetworkFabricIPExtendedCommunityResource, please refer to the document of NetworkFabricIPExtendedCommunityResource
            string subscriptionId = "1234ABCD-0A1B-1234-5678-123456ABCDEF";
            string resourceGroupName = "example-rg";
            string ipExtendedCommunityName = "example-ipExtendedCommunity";
            ResourceIdentifier networkFabricIPExtendedCommunityResourceId = NetworkFabricIPExtendedCommunityResource.CreateResourceIdentifier(subscriptionId, resourceGroupName, ipExtendedCommunityName);
            NetworkFabricIPExtendedCommunityResource networkFabricIPExtendedCommunity = client.GetNetworkFabricIPExtendedCommunityResource(networkFabricIPExtendedCommunityResourceId);

            // invoke the operation
            await networkFabricIPExtendedCommunity.DeleteAsync(WaitUntil.Completed);

            Console.WriteLine($"Succeeded");
        }

        // IpExtendedCommunities_ListBySubscription_MaximumSet_Gen
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("Only verifying that the sample builds")]
        public async Task GetNetworkFabricIPExtendedCommunities_IpExtendedCommunitiesListBySubscriptionMaximumSetGen()
        {
            // Generated from example definition: specification/managednetworkfabric/resource-manager/Microsoft.ManagedNetworkFabric/stable/2023-06-15/examples/IpExtendedCommunities_ListBySubscription_MaximumSet_Gen.json
            // this example is just showing the usage of "IpExtendedCommunities_ListBySubscription" operation, for the dependent resources, they will have to be created separately.

            // get your azure access token, for more details of how Azure SDK get your access token, please refer to https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication?tabs=command-line
            TokenCredential cred = new DefaultAzureCredential();
            // authenticate your client
            ArmClient client = new ArmClient(cred);

            // this example assumes you already have this SubscriptionResource created on azure
            // for more information of creating SubscriptionResource, please refer to the document of SubscriptionResource
            string subscriptionId = "1234ABCD-0A1B-1234-5678-123456ABCDEF";
            ResourceIdentifier subscriptionResourceId = SubscriptionResource.CreateResourceIdentifier(subscriptionId);
            SubscriptionResource subscriptionResource = client.GetSubscriptionResource(subscriptionResourceId);

            // invoke the operation and iterate over the result
            await foreach (NetworkFabricIPExtendedCommunityResource item in subscriptionResource.GetNetworkFabricIPExtendedCommunitiesAsync())
            {
                // the variable item is a resource, you could call other operations on this instance as well
                // but just for demo, we get its data from this resource instance
                NetworkFabricIPExtendedCommunityData resourceData = item.Data;
                // for demo we just print out the id
                Console.WriteLine($"Succeeded on id: {resourceData.Id}");
            }

            Console.WriteLine($"Succeeded");
        }
    }
}
