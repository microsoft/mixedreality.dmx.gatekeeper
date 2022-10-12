// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Tests.Acceptance.Models.LabCommands;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.LabCommands
{
    public partial class LabCommandApiTests : IDisposable
    {
        [Fact]
        public async Task ShouldPostLabCommandsAsync()
        {
            // given
            LabCommand randomLabCommand = CreateRandomLabCommand();
            LabCommand expectedLabCommand = randomLabCommand.DeepClone();

            string randomLabCommandBody =
                JsonConvert.SerializeObject(randomLabCommand);

            this.wireMockServer
                .Given(Request.Create()
                    .WithPath("/api/labcommands")
                    .WithBody(randomLabCommandBody)
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabCommandBody));

            // when
            LabCommand actualLabCommands =
                await this.dmxGatekeeperApiBroker.PostLabCommand(randomLabCommand);

            // then
            actualLabCommands.Should().BeEquivalentTo(expectedLabCommand);
        }

        [Fact]
        public async Task ShouldRetrieveLabCommandsAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            LabCommand randomLabCommand = CreateRandomLabCommand();
            LabCommand expectedLabCommand = randomLabCommand.DeepClone();

            string randomLabCommandBody =
                JsonConvert.SerializeObject(randomLabCommand);

            this.wireMockServer
                .Given(Request.Create()
                    .WithPath($"/api/labcommands/{randomId}")
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabCommandBody));

            // when
            LabCommand actualLabCommands =
                await this.dmxGatekeeperApiBroker.GetLabCommandById(randomId);

            // then
            actualLabCommands.Should().BeEquivalentTo(expectedLabCommand);
        }

        public void Dispose() => this.wireMockServer.Stop();
    }
}
