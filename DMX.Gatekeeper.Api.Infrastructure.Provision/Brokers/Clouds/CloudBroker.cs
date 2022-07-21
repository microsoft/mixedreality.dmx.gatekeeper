// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace DMX.Gatekeeper.Api.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker : ICloudBroker
    {
        private const string ProjectEnvironment = "Production";
        private readonly string tenantId;
        private readonly string dmxGatekeeperClientId;
        private readonly string dmxGatekeeperInstance;
        private readonly string dmxGatekeeperDomain;
        private readonly string dmxGatekeeperCallbackPath;
        private readonly string dmxGatekeeperScopesGetAllLabs;
        private readonly string dmxGatekeeperScopesPostLab;
        private readonly string dmxCoreAppIdUri;
        private readonly string dmxCoreAppScopesGetAllLabs;
        private readonly string dmxCoreAppScopesPostLab;
        private readonly string provisionClientId;
        private readonly string provisionClientSecret;
        private readonly string configurationDmxCoreApiUrl;
        private readonly IAzure azure;

        public CloudBroker()
        {
            this.tenantId = Environment.GetEnvironmentVariable("AzureTenantId");
            this.dmxGatekeeperClientId = Environment.GetEnvironmentVariable("AzureAdAppDmxGatekeeperClientId");
            this.dmxGatekeeperInstance = Environment.GetEnvironmentVariable("AzureAdAppDmxGatekeeperInstance");
            this.dmxGatekeeperDomain = Environment.GetEnvironmentVariable("AzureAdAppDmxGatekeeperDomain");
            this.dmxGatekeeperCallbackPath = Environment.GetEnvironmentVariable("AzureAdAppDmxGatekeeperCallbackPath");
            this.dmxGatekeeperScopesGetAllLabs = Environment.GetEnvironmentVariable("AzureAdAppDmxGatekeeperScopesGetAllLabs");
            this.dmxGatekeeperScopesPostLab = Environment.GetEnvironmentVariable("AzureAdAppDmxGatekeeperScopesPostLab");
            this.dmxCoreAppIdUri = Environment.GetEnvironmentVariable("AzureAdAppDmxCoreAppIdUri");
            this.dmxCoreAppScopesGetAllLabs = Environment.GetEnvironmentVariable("AzureAdAppDmxCoreAppScopesGetAllLabs");
            this.dmxCoreAppScopesPostLab = Environment.GetEnvironmentVariable("AzureAdAppDmxCoreAppScopesGetAllLabs");
            this.provisionClientId = Environment.GetEnvironmentVariable("AzureAdAppProvisionClientId");
            this.provisionClientSecret = Environment.GetEnvironmentVariable("AzureAdAppProvisionClientSecret");
            this.configurationDmxCoreApiUrl = Environment.GetEnvironmentVariable("AzureAppServiceDmxCoreApiUrl");
            this.azure = AuthenticateAzure();
        }

        private IAzure AuthenticateAzure()
        {
            AzureCredentials credentials =
                SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                    clientId: this.provisionClientId,
                    clientSecret: this.provisionClientSecret,
                    tenantId: this.tenantId,
                    environment: AzureEnvironment.AzureGlobalCloud);

            return Azure.Configure()
                .WithLogLevel(level: HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();
        }
    }
}
