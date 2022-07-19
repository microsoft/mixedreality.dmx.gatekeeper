// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace DMX.Gatekeeper.Api.Brokers.Tokens
{
    public partial interface ITokenAcquisitionBroker
    {
        ValueTask<string> GetAccessTokenForUserByScopeCategoryAsync(string scopeCategory);
    }
}
