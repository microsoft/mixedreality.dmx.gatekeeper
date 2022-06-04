// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.Labs;
using FluentAssertions;
using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.Labs
{
    public partial class LabApiTests
    {
        [Fact]
        public async Task ShouldRetrieveAllLabsAsync()
        {
            //given
            List<Lab> randomLabs = CreateRandomLabs();
            List<Lab> expectedLabs = randomLabs;

            string randomLabsCollectionBody =
                JsonConvert.SerializeObject(randomLabs);

            this.wireMockServer
                .Given(Request.Create()
                    .WithPath("/api/labs")
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabsCollectionBody));

            //when
            List<Lab> actualLabs =
                await this.dmxGatekeeperApiBroker.GetAllLabs();

            //then
            actualLabs.Should().BeEquivalentTo(expectedLabs);
        }
    }
}
