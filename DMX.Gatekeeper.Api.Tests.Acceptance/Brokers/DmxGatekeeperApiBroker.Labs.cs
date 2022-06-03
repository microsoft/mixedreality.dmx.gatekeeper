// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.Labs;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.Brokers
{
    public partial class DmxGatekeeperApiBroker
    {
        private const string AllLabsApiRelativeUrl = "api/labs";

        public async ValueTask<List<Lab>> GetAllLabs()
        {
            return await this.apiFactoryClient.GetContentAsync<List<Lab>>(
                relativeUrl: $"{AllLabsApiRelativeUrl}");
        }
    }
}
