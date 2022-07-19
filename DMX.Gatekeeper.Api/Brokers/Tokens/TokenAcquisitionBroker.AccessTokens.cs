// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace DMX.Gatekeeper.Api.Brokers.Tokens
{
    public partial class TokenAcquisitionBroker
    {
        public async ValueTask<string> GetAccessTokenForUserByScopeCategoryAsync(
            string scopeCategory)
        {
            string[] scopes = this.GetScopesFromConfiguration(scopeCategory);

            return await tokenAcquisition.GetAccessTokenForUserAsync(scopes);
        }
    }
}
