// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Infrastructure.Provision.Models.Configurations;

namespace DMX.Gatekeeper.Api.Infrastructure.Provision.Brokers.Configurations
{
    public interface IConfigurationBroker
    {
        CloudManagementConfiguration GetConfiguration();
    }
}
