// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

namespace DMX.Gatekeeper.Api.Infrastructure.Provision.Models.Configurations
{
    public class CloudManagementConfiguration
    {
        public string ProjectName { get; set; }
        public CloudAction Up { get; set; }
        public CloudAction Down { get; set; }
    }
}
