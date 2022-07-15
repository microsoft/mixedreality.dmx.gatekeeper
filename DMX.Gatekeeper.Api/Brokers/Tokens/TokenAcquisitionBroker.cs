// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

namespace DMX.Gatekeeper.Api.Brokers.Tokens
{
    public partial class TokenAcquisitionBroker : ITokenAcquisitionBroker
    {
        private ITokenAcquisition tokenAcquisition;

        public TokenAcquisitionBroker(
            ITokenAcquisition tokenAcquisition, 
            IConfiguration configuration)
        {
            this.tokenAcquisition = tokenAcquisition;
            
        }

        public async ValueTask<string> GetAccessTokenForUserAsync(string[] scopes) =>
            await tokenAcquisition.GetAccessTokenForUserAsync(scopes);
        }
}
