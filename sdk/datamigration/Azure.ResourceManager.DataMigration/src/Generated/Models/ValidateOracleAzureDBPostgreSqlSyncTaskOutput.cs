// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.DataMigration.Models
{
    /// <summary> Output for task that validates migration input for Oracle to Azure Database for PostgreSQL for online migrations. </summary>
    public partial class ValidateOracleAzureDBPostgreSqlSyncTaskOutput
    {
        /// <summary> Initializes a new instance of ValidateOracleAzureDBPostgreSqlSyncTaskOutput. </summary>
        internal ValidateOracleAzureDBPostgreSqlSyncTaskOutput()
        {
            ValidationErrors = new ChangeTrackingList<ReportableException>();
        }

        /// <summary> Initializes a new instance of ValidateOracleAzureDBPostgreSqlSyncTaskOutput. </summary>
        /// <param name="validationErrors"> Errors associated with a selected database object. </param>
        internal ValidateOracleAzureDBPostgreSqlSyncTaskOutput(IReadOnlyList<ReportableException> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        /// <summary> Errors associated with a selected database object. </summary>
        public IReadOnlyList<ReportableException> ValidationErrors { get; }
    }
}
