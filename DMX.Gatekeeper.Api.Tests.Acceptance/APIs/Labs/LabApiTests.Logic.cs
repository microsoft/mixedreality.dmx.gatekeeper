// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.Labs;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.Labs
{
    public partial class LabApiTests : IDisposable
    {
        [Fact]
        public async Task ShouldPostLabsAsync()
        {
            // given
            Lab randomLab = CreateRandomLab();
            Lab expectedLab = randomLab.DeepClone();

            string randomLabBody =
                JsonConvert.SerializeObject(randomLab);
            
            this.wireMockServer
                .Given(Request.Create()
                    .WithPath("/api/labs")
                    .WithBody(randomLabBody)
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabBody));

            // when
            Lab actualLabs =
                await this.dmxGatekeeperApiBroker.PostLab(randomLab);

            // then
            actualLabs.Should().BeEquivalentTo(expectedLab);
        }

        [Fact]
        public async Task ShouldRetrieveAllLabsAsync()
        {
            // given
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

            // when
            List<Lab> actualLabs =
                await this.dmxGatekeeperApiBroker.GetAllLabs();

            // then
            actualLabs.Should().BeEquivalentTo(expectedLabs);
        }

        public void Dispose()
        {
            this.wireMockServer.Stop();
        }
    }
}
