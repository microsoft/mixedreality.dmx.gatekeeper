// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

namespace DMX.Gatekeeper.Api.Infrastructure.Build.Services.ScriptGenerations
{
    public class ScriptGenerationService
    {
        private readonly ADotNetClient adotNetClient;

        public ScriptGenerationService() =>
            this.adotNetClient = new ADotNetClient();

        public void GenerateBuildScript()
        {
            var githubPipeline = new GithubPipeline
            {
                Name = "Gatekeeper Build",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { "main" }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "main" }
                    }
                },

                Jobs = new Jobs
                {
                    Build = new BuildJob
                    {
                        RunsOn = BuildMachines.Windows2022,

                        Steps = new List<GithubTask>
                {
                    new CheckoutTaskV2
                    {
                        Name = "Checking out Code"
                    },

                    new SetupDotNetTaskV1
                    {
                        Name = "Installing .NET",

                        TargetDotNetVersion = new TargetDotNetVersion
                        {
                            DotNetVersion = "7.0.100-preview.4.22252.9",
                            IncludePrerelease = true
                        }
                    },

                    new RestoreTask
                    {
                        Name = "Restoring Packages"
                    },

                    new DotNetBuildTask
                    {
                        Name = "Building Project(s)"
                    },

                    new TestTask
                    {
                        Name = "Running Tests"
                    }
                }
                    }
                }
            };

            var adotNetClient = new ADotNetClient();

            adotNetClient.SerializeAndWriteToFile(
                githubPipeline,
                path: "../../../../.github/workflows/dotnet.yml");
        }

        public void GenerateProvisionScript()
        {
            var githubPipeline = new GithubPipeline
            {
                Name = "Provision DMX Gatekeeper",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { "main" }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "main" }
                    }
                },

                Jobs = new Jobs
                {
                    Build = new BuildJob
                    {
                        RunsOn = BuildMachines.WindowsLatest,

                        EnvironmentVariables = new Dictionary<string, string>
                        {
                            { "AzureSubscriptionId", "${{ secrets.AZURE_SUBSCRIPTIONID }}"},
                            { "AzureTenantId", "${{ secrets.AZURE_TENANTID }}" },
                            { "AzureAdAppProvisionClientId", "${{ secrets.AZURE_ADAPP_PROVISION_CLIENTID }}" },
                            { "AzureAdAppProvisionClientSecret", "${{ secrets.AZURE_ADAPP_PROVISION_CLIENTSECRET }}" },
                            { "AzureAdAppDmxGatekeeperClientId", "${{ secrets.AZURE_ADAPP_DMXGATEKEEPER_CLIENTID }}" },
                            { "AzureAdAppDmxGatekeeperClientSecret", "${{ secrets.AZURE_ADAPP_DMXGATEKEEPER_CLIENTSECRET }}" },
                            { "AzureAdAppDmxGatekeeperInstance", "${{ secrets.AZURE_ADAPP_DMXGATEKEEPER_INSTANCE }}" },
                            { "AzureAdAppDmxGatekeeperDomain", "${{ secrets.AZURE_ADAPP_DMXGATEKEEPER_DOMAIN }}" },
                            { "AzureAdAppDmxGatekeeperCallbackPath", "${{ secrets.AZURE_ADAPP_DMXGATEKEEPER_CALLBACKPATH }}" },
                            { "AzureAdAppDmxGatekeeperScopesGetAllLabs", "${{ secrets.AZURE_ADAPP_DMXGATEKEEPER_SCOPES_GETALLLABS }}" },
                            { "AzureAdAppDmxGatekeeperScopesPostLab", "${{ secrets.AZURE_ADAPP_DMXGATEKEEPER_SCOPES_POSTLAB }}" },
                            { "AzureAdAppDmxCoreAppIdUri", "${{ secrets.AZURE_ADAPP_DMXCORE_APPIDURI }}" },
                            { "AzureAdAppDmxCoreAppScopesGetAllLabs", "${{ secrets.AZURE_ADAPP_DMXCORE_APPSCOPES_GETALLLABS }}" },
                            { "AzureAdAppDmxCoreAppScopesPostLab", "${{ secrets.AZURE_ADAPP_DMXCORE_APPSCOPES_POSTLAB }}" },
                            { "AzureAppServiceDmxCoreApiUrl", "${{ secrets.AZURE_APPSERVICE_DMXCOREAPI_URL }}" },
                        },

                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV2
                            {
                                Name = "Check Out"
                            },

                            new SetupDotNetTaskV1
                            {
                                Name = "Setup Dot Net Version",

                                TargetDotNetVersion = new TargetDotNetVersion
                                {
                                    DotNetVersion = "7.0.100-preview.1.22110.4",
                                    IncludePrerelease = true
                                }
                            },

                            new RestoreTask
                            {
                                Name = "Restore"
                            },

                            new DotNetBuildTask
                            {
                                Name = "Build"
                            },

                            new RunTask
                            {
                                Name = "Provision",
                                Run = "dotnet run --project .\\DMX.Gatekeeper.Api.Infrastructure.Provision\\DMX.Gatekeeper.Api.Infrastructure.Provision.csproj"
                            }
                        }
                    }
                }
            };

            this.adotNetClient.SerializeAndWriteToFile(
                githubPipeline,
                path: "../../../../.github/workflows/provision.yml");
        }
    }
}
