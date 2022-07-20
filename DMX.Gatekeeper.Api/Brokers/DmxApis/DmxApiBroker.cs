// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using RESTFulSense.Clients;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial class DmxApiBroker : IDmxApiBroker
    {
        private readonly IRESTFulApiFactoryClient apiClient;
        private HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly ITokenAcquisition tokenAcquisition;

        public DmxApiBroker(
            HttpClient httpClient, 
            IConfiguration configuration,
            ITokenAcquisition tokenAcquisition)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.tokenAcquisition = tokenAcquisition;
            this.apiClient = GetApiClient(configuration);
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        private async ValueTask<T> PostAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PostContentAsync<T>(relativeUrl, content);

        private IRESTFulApiFactoryClient GetApiClient(IConfiguration configuration)
        {
            LocalConfiguration localConfigurations =
                configuration.Get<LocalConfiguration>();

            string apiBaseUrl = localConfigurations.ApiConfiguration.Url;
            this.httpClient.BaseAddress = new Uri(apiBaseUrl);

            return new RESTFulApiFactoryClient(this.httpClient);
        }
        
        private string[] GetScopesFromConfiguration(string scopeCategory)
        {
            LocalConfiguration localConfiguration =
                this.configuration.Get<LocalConfiguration>();

            localConfiguration.DownstreamApi.Scopes.TryGetValue(
                scopeCategory, out string scopes);

            return scopes.Split();
        }
    }
}