// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.Labs;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabsRelativeUrl = "api/labs";

        public async ValueTask<Lab> PostLabAsync(Lab lab)
        {
            string[] requiredScopes = this.GetScopesFromConfiguration(
                scopeCategory: "PostLab");

            string accessToken =
                await this.tokenAcquisition.GetAccessTokenForUserAsync(requiredScopes);

            this.httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            return await PostAsync<Lab>(LabsRelativeUrl, lab);
        }

        public async ValueTask<List<Lab>> GetAllLabsAsync()
        {
            string[] requiredScopes = this.GetScopesFromConfiguration(
                scopeCategory: "GetAllLabs");

            string accessToken =
                await this.tokenAcquisition.GetAccessTokenForUserAsync(requiredScopes);

            this.httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            return await GetAsync<List<Lab>>(LabsRelativeUrl);
        }
    }
}