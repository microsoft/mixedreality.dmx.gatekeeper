// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Brokers;
using FluentAssertions;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.Homes
{
    [Collection(nameof(ApiTestCollection))]
    public class HomeApiTests
    {
        private readonly DmxGatekeeperApiBroker dmxGatekeeperApiBroker;

        public HomeApiTests(DmxGatekeeperApiBroker dmxGatekeeperApiBroker) =>
            this.dmxGatekeeperApiBroker = dmxGatekeeperApiBroker;

        [Fact]
        public async Task ShouldReturnHomeMessageAsync()
        {
            string expectedMessage = "Hello, Goodbye";

            string actualMessage =
                await this.dmxGatekeeperApiBroker.GetHomeMessageAsync();

            actualMessage.Should().BeEquivalentTo(expectedMessage);
        }
    }
}