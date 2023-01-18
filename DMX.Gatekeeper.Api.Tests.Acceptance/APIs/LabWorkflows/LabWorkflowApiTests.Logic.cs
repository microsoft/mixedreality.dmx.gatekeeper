// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Net;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.LabWorkflows;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.LabWorkflows
{
    public partial class LabWorkflowApiTests : IDisposable
    {

        [Fact]
        public async Task ShouldPostLabWorkflowsAsync()
        {
            // given
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow expectedLabWorkflow = randomLabWorkflow.DeepClone();

            string randomLabWorkflowBody =
                JsonConvert.SerializeObject(randomLabWorkflow);

            this.wireMockServer
                .Given(Request.Create()
                    .WithPath("/api/labworkflows")
                    .WithBody(randomLabWorkflowBody)
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabWorkflowBody));

            // when
            LabWorkflow actualLabWorkflows =
                await this.dmxGatekeeperApiBroker.PostLabWorkflow(randomLabWorkflow);

            // then
            actualLabWorkflows.Should().BeEquivalentTo(expectedLabWorkflow);
        }

        [Fact]
        public async Task ShouldRetrieveLabWorkflowByIdAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow expectedLabWorkflow = randomLabWorkflow.DeepClone();

            string randomLabWorkflowBody =
                JsonConvert.SerializeObject(randomLabWorkflow);

            this.wireMockServer
                .Given(Request.Create()
                    .WithPath($"/api/labworkflows/{randomId}")
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(randomLabWorkflowBody));

            // when
            LabWorkflow actualLabWorkflows =
                await this.dmxGatekeeperApiBroker.GetLabWorkflowById(randomId);

            // then
            actualLabWorkflows.Should().BeEquivalentTo(expectedLabWorkflow);
        }

        public void Dispose() => this.wireMockServer.Stop();
    }
}
