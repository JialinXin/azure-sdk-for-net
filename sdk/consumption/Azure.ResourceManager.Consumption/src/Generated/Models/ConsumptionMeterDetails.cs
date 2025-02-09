// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.Consumption.Models
{
    /// <summary> The properties of the meter detail. </summary>
    public partial class ConsumptionMeterDetails
    {
        /// <summary> Initializes a new instance of ConsumptionMeterDetails. </summary>
        internal ConsumptionMeterDetails()
        {
        }

        /// <summary> Initializes a new instance of ConsumptionMeterDetails. </summary>
        /// <param name="meterName"> The name of the meter, within the given meter category. </param>
        /// <param name="meterCategory"> The category of the meter, for example, 'Cloud services', 'Networking', etc.. </param>
        /// <param name="meterSubCategory"> The subcategory of the meter, for example, 'A6 Cloud services', 'ExpressRoute (IXP)', etc.. </param>
        /// <param name="unit"> The unit in which the meter consumption is charged, for example, 'Hours', 'GB', etc. </param>
        /// <param name="meterLocation"> The location in which the Azure service is available. </param>
        /// <param name="totalIncludedQuantity"> The total included quantity associated with the offer. </param>
        /// <param name="pretaxStandardRate"> The pretax listing price. </param>
        /// <param name="serviceName"> The name of the service. </param>
        /// <param name="serviceTier"> The service tier. </param>
        internal ConsumptionMeterDetails(string meterName, string meterCategory, string meterSubCategory, string unit, string meterLocation, decimal? totalIncludedQuantity, decimal? pretaxStandardRate, string serviceName, string serviceTier)
        {
            MeterName = meterName;
            MeterCategory = meterCategory;
            MeterSubCategory = meterSubCategory;
            Unit = unit;
            MeterLocation = meterLocation;
            TotalIncludedQuantity = totalIncludedQuantity;
            PretaxStandardRate = pretaxStandardRate;
            ServiceName = serviceName;
            ServiceTier = serviceTier;
        }

        /// <summary> The name of the meter, within the given meter category. </summary>
        public string MeterName { get; }
        /// <summary> The category of the meter, for example, 'Cloud services', 'Networking', etc.. </summary>
        public string MeterCategory { get; }
        /// <summary> The subcategory of the meter, for example, 'A6 Cloud services', 'ExpressRoute (IXP)', etc.. </summary>
        public string MeterSubCategory { get; }
        /// <summary> The unit in which the meter consumption is charged, for example, 'Hours', 'GB', etc. </summary>
        public string Unit { get; }
        /// <summary> The location in which the Azure service is available. </summary>
        public string MeterLocation { get; }
        /// <summary> The total included quantity associated with the offer. </summary>
        public decimal? TotalIncludedQuantity { get; }
        /// <summary> The pretax listing price. </summary>
        public decimal? PretaxStandardRate { get; }
        /// <summary> The name of the service. </summary>
        public string ServiceName { get; }
        /// <summary> The service tier. </summary>
        public string ServiceTier { get; }
    }
}
