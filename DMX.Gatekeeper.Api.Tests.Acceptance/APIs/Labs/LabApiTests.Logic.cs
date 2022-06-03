// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.Labs;
using Force.DeepCloner;
using Microsoft.Extensions.DependencyModel;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;
using Newtonsoft.Json;
using FluentAssertions;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.Labs
{
    public partial class LabApiTests
    {
        [Fact]
        public async Task ShouldRetrieveAllLabsAsync()
        {
            //given
            List<Lab> randomLabs = CreateRandomLabsData();

            string randomLabCollectionBody =
                JsonConvert.SerializeObject(randomLabs.ToArray());

            List<Lab> expectedRandomLabs = randomLabs.DeepClone();

            this.wireMockServer
                .Given(Request.Create()
                    .WithPath("/api/labs")
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabCollectionBody));

            //when
            List<Lab> actualLabs = await this.dmxGatekeeperApiBroker.GetAllLabs();

            //then
            actualLabs.Should().BeEquivalentTo(expectedRandomLabs);
        }
    }
}
