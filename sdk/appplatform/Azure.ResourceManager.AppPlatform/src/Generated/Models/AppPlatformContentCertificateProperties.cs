// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;

namespace Azure.ResourceManager.AppPlatform.Models
{
    /// <summary> Properties of certificate imported from key vault. </summary>
    public partial class AppPlatformContentCertificateProperties : AppPlatformCertificateProperties
    {
        /// <summary> Initializes a new instance of AppPlatformContentCertificateProperties. </summary>
        public AppPlatformContentCertificateProperties()
        {
            CertificatePropertiesType = "ContentCertificate";
        }

        /// <summary> Initializes a new instance of AppPlatformContentCertificateProperties. </summary>
        /// <param name="certificatePropertiesType"> The type of the certificate source. </param>
        /// <param name="thumbprint"> The thumbprint of certificate. </param>
        /// <param name="issuer"> The issuer of certificate. </param>
        /// <param name="issuedOn"> The issue date of certificate. </param>
        /// <param name="expireOn"> The expiration date of certificate. </param>
        /// <param name="activateOn"> The activate date of certificate. </param>
        /// <param name="subjectName"> The subject name of certificate. </param>
        /// <param name="dnsNames"> The domain list of certificate. </param>
        /// <param name="provisioningState"> Provisioning state of the Certificate. </param>
        /// <param name="content"> The content of uploaded certificate. </param>
        internal AppPlatformContentCertificateProperties(string certificatePropertiesType, string thumbprint, string issuer, DateTimeOffset? issuedOn, DateTimeOffset? expireOn, DateTimeOffset? activateOn, string subjectName, IReadOnlyList<string> dnsNames, AppPlatformCertificateProvisioningState? provisioningState, string content) : base(certificatePropertiesType, thumbprint, issuer, issuedOn, expireOn, activateOn, subjectName, dnsNames, provisioningState)
        {
            Content = content;
            CertificatePropertiesType = certificatePropertiesType ?? "ContentCertificate";
        }

        /// <summary> The content of uploaded certificate. </summary>
        public string Content { get; set; }
    }
}
