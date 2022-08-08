// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace DMX.Gatekeeper.Api.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<IWebApp> CreateWebAppAsync(
            string webAppName,
            IAppServicePlan appServicePlan,
            IResourceGroup resourceGroup)
        {
            var webAppSettings = new Dictionary<string, string>
                {
                    { "ASPNETCORE_ENVIRONMENT", ProjectEnvironment },
                    { "ApiConfiguration:Url", this.configurationDmxCoreApiUrl },
                    { "AzureAd:TenantId", this.tenantId },
                    { "AzureAd:Instance", this.dmxGatekeeperInstance },
                    { "AzureAd:Domain", this.dmxGatekeeperDomain },
                    { "AzureAd:ClientId", this.dmxGatekeeperClientId },
                    { "AzureAd:ClientSecret", this.dmxGatekeeperClientSecret },
                    { "AzureAd:CallbackPath", this.dmxGatekeeperCallbackPath },
                    { "AzureAd:Scopes:GetAllLabs", this.dmxGatekeeperScopesGetAllLabs },
                    { "AzureAd:Scopes:PostLab", this.dmxGatekeeperScopesPostLab },
                    { "AzureAd:Scopes:PostLabCommand", this.dmxGatekeeperScopesPostLabCommand },
                    { "DownstreamApi:BaseUrl", this.dmxCoreAppIdUri },
                    { "DownstreamApi:Scopes:GetAllLabs", this.dmxCoreAppScopesGetAllLabs },
                    { "DownstreamApi:Scopes:PostLab", this.dmxCoreAppScopesPostLab },
                    { "DownstreamApi:Scopes:PostLabCommand", this.dmxCoreAppScopesPostLabCommand },
                };

            return await this.azure.AppServices.WebApps
                .Define(webAppName)
                .WithExistingWindowsPlan(appServicePlan)
                .WithExistingResourceGroup(resourceGroup)
                .WithNetFrameworkVersion(NetFrameworkVersion.Parse("v6.0"))
                .WithAppSettings(settings: webAppSettings)
                .CreateAsync();
        }
    }
}
