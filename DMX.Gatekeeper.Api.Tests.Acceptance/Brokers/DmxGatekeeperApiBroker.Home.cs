﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.Brokers
{
    public partial class DmxGatekeeperApiBroker
    {
        private const string HomeRelatativeUrl = "api/home";

        public async ValueTask<string> GetHomeMessageAsync() =>
            await this.apiFactoryClient.GetContentStringAsync(HomeRelatativeUrl);
    }
}