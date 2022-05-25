// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Infrastructure.Provision.Brokers.Clouds;
using DMX.Gatekeeper.Api.Infrastructure.Provision.Brokers.Loggings;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace DMX.Gatekeeper.Api.Infrastructure.Provision.Services.Foundations.CloudManagements
{
    public class CloudManagementService : ICloudManagementService
    {
        private readonly ICloudBroker cloudBroker;
        private readonly ILoggingBroker loggingBroker;

        public CloudManagementService()
        {
            this.cloudBroker = new CloudBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public async ValueTask<IResourceGroup> ProvisionResourceGroupAsync(
            string projectName,
            string environment)
        {
            string resourceGroupName =
                $"{projectName}-RESOURCES-{environment}".ToUpper();

            this.loggingBroker.LogActivity(
                message: $"Provisioning {resourceGroupName} ...");

            IResourceGroup resourceGroup =
                await this.cloudBroker.CreateResourceGroupAsync(
                    resourceGroupName);

            this.loggingBroker.LogActivity(
                $"Provisioning {resourceGroupName} Completed.");

            return resourceGroup;
        }

        public async ValueTask<IAppServicePlan> ProvisionAppServicePlanAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup)
        {
            string appServicePlanName = $"{projectName}-PLAN-{environment}".ToUpper();
            this.loggingBroker.LogActivity(message: $"Provisioning {appServicePlanName} ...");

            IAppServicePlan appServicePlan = await this.cloudBroker.CreatePlanAsync(
                appServicePlanName, resourceGroup);

            this.loggingBroker.LogActivity(message: $"Provisioning {appServicePlanName} complete.");

            return appServicePlan;
        }

        public async ValueTask<IWebApp> ProvisionWebAppAsync(
            string projectName,
            string environment,
            IAppServicePlan appServicePlan,
            IResourceGroup resourceGroup)
        {
            string webAppName = $"{projectName}-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisoning {webAppName}...");

            IWebApp webApp = await this.cloudBroker.CreateWebAppAsync(
                webAppName,
                appServicePlan,
                resourceGroup);

            this.loggingBroker.LogActivity(
                message: $"Provisioning {webAppName} complete.");

            return webApp;
        }

        public async ValueTask DeprovisionResourceGroupAsync(string projectName, string environment)
        {
            string resourceGroupName =
                $"{projectName}-RESOURCES-{environment}".ToUpper();

            this.loggingBroker.LogActivity(
                message: $"Checking for {resourceGroupName} ...");

            bool isResourceGroupExist =
                await this.cloudBroker.CheckResourceGroupExistsAsync(resourceGroupName);

            if (isResourceGroupExist)
            {
                this.loggingBroker.LogActivity(
                     message: $"Deprovisioning {resourceGroupName} ...");

                await this.cloudBroker.DeleteResourceGroupAsync(
                     resourceGroupName);

                this.loggingBroker.LogActivity(
                    message: $"Deprovisioning {resourceGroupName} Completed.");
            }
            else
            {
                this.loggingBroker.LogActivity(
                    message: $"Could not find {resourceGroupName}.");
            }
        }
    }
}
