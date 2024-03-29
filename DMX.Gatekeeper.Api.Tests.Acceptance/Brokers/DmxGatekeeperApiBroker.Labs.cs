﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.Labs;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.Brokers
{
    public partial class DmxGatekeeperApiBroker
    {
        private const string AllLabsApiRelativeUrl = "api/labs";

        public async ValueTask<Lab> PostLab(Lab lab)
        {
            return await this.apiFactoryClient.PostContentAsync<Lab>(
                relativeUrl: $"{AllLabsApiRelativeUrl}",
                content: lab);
        }

        public async ValueTask<List<Lab>> GetAllLabs()
        {
            return await this.apiFactoryClient.GetContentAsync<List<Lab>>(
                relativeUrl: $"{AllLabsApiRelativeUrl}");
        }
    }
}
