// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.Subscription.Models
{
    /// <summary> Put billing account policies response properties. </summary>
    public partial class BillingAccountPolicyProperties
    {
        /// <summary> Initializes a new instance of BillingAccountPolicyProperties. </summary>
        internal BillingAccountPolicyProperties()
        {
            ServiceTenants = new ChangeTrackingList<ServiceTenant>();
        }

        /// <summary> Initializes a new instance of BillingAccountPolicyProperties. </summary>
        /// <param name="serviceTenants"> Service tenant for the billing account. </param>
        /// <param name="allowTransfers"> Determine if the transfers are allowed for the billing account. </param>
        internal BillingAccountPolicyProperties(IReadOnlyList<ServiceTenant> serviceTenants, bool? allowTransfers)
        {
            ServiceTenants = serviceTenants;
            AllowTransfers = allowTransfers;
        }

        /// <summary> Service tenant for the billing account. </summary>
        public IReadOnlyList<ServiceTenant> ServiceTenants { get; }
        /// <summary> Determine if the transfers are allowed for the billing account. </summary>
        public bool? AllowTransfers { get; }
    }
}
