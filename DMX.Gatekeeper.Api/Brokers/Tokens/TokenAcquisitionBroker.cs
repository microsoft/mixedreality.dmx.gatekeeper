// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

namespace DMX.Gatekeeper.Api.Brokers.Tokens
{
    public partial class TokenAcquisitionBroker : ITokenAcquisitionBroker
    {
        private readonly IConfiguration configuration;
        private ITokenAcquisition tokenAcquisition;

        public TokenAcquisitionBroker(
            ITokenAcquisition tokenAcquisition,
            IConfiguration configuration)
        {
            this.tokenAcquisition = tokenAcquisition;
            this.configuration = configuration;

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
