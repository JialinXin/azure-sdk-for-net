// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.RecoveryServicesBackup.Models
{
    /// <summary> IaaS VM workload-specific backup request. </summary>
    public partial class IaasVmBackupContent : BackupContent
    {
        /// <summary> Initializes a new instance of IaasVmBackupContent. </summary>
        public IaasVmBackupContent()
        {
            ObjectType = "IaasVMBackupRequest";
        }

        /// <summary> Initializes a new instance of IaasVmBackupContent. </summary>
        /// <param name="objectType"> This property will be used as the discriminator for deciding the specific types in the polymorphic chain of types. </param>
        /// <param name="recoveryPointExpireOn"> Backup copy will expire after the time specified (UTC). </param>
        internal IaasVmBackupContent(string objectType, DateTimeOffset? recoveryPointExpireOn) : base(objectType)
        {
            RecoveryPointExpireOn = recoveryPointExpireOn;
            ObjectType = objectType ?? "IaasVMBackupRequest";
        }

        /// <summary> Backup copy will expire after the time specified (UTC). </summary>
        public DateTimeOffset? RecoveryPointExpireOn { get; set; }
    }
}
