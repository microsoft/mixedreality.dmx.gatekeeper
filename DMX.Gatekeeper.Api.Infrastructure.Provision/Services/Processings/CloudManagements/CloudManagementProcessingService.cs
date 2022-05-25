// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Infrastructure.Provision.Brokers.Configurations;
using DMX.Gatekeeper.Api.Infrastructure.Provision.Models.Configurations;
using DMX.Gatekeeper.Api.Infrastructure.Provision.Services.Foundations.CloudManagements;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace DMX.Gatekeeper.Api.Infrastructure.Provision.Services.Processings.CloudManagements
{
    public class CloudManagementProcessingService : ICloudManagementProcessingService
    {
        private readonly IConfigurationBroker configurationBroker;
        private readonly ICloudManagementService cloudMangementService;

        public CloudManagementProcessingService()
        {
            this.configurationBroker = new ConfigurationBroker();
            this.cloudMangementService = new CloudManagementService();
        }

        public async ValueTask ProcessAsync()
        {
            CloudManagementConfiguration configuration =
                this.configurationBroker.GetConfiguration();

            await ProvisionResourcesAsync(configuration);
            await DeprovisionResourcesAsync(configuration);
        }

        private async Task ProvisionResourcesAsync(CloudManagementConfiguration configuration)
        {
            string projectName = configuration.ProjectName;

            List<string> provisionEnvironments =
                RetrieveEnvironments(configuration.Up);

            foreach (string environment in provisionEnvironments)
            {
                IResourceGroup resourceGroup = await this.cloudMangementService
                    .ProvisionResourceGroupAsync(
                        projectName,
                        environment);

                IAppServicePlan appServicePlan = await this.cloudMangementService
                    .ProvisionAppServicePlanAsync(
                        projectName,
                        environment,
                        resourceGroup);

                IWebApp webApp = await this.cloudMangementService
                    .ProvisionWebAppAsync(
                        projectName,
                        environment,
                        appServicePlan,
                        resourceGroup);
            }
        }

        private async Task DeprovisionResourcesAsync(
            CloudManagementConfiguration configuration)
        {
            string projectName = configuration.ProjectName;

            List<string> deprovisionEnvironments =
                RetrieveEnvironments(configuration.Down);

            foreach (string environment in deprovisionEnvironments)
            {
                await this.cloudMangementService.DeprovisionResourceGroupAsync(
                    projectName,
                    environment);
            }
        }

        private static List<string> RetrieveEnvironments(CloudAction cloudAction) =>
            cloudAction?.Environments ?? new List<string>();
    }
}
