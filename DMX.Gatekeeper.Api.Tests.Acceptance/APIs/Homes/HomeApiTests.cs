// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

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
            // given
            string expectedMessage = "Hello, Goodbye";

            // when
            string actualMessage =
                await this.dmxGatekeeperApiBroker.GetHomeMessageAsync();

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage);
        }
    }
}