// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        [Fact]
        public async Task ShouldRetreieveLabWorkflowByIdAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputId = randomId;
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow retrievedLabWorkflow = randomLabWorkflow.DeepClone();
            LabWorkflow expectedLabWorkflow = retrievedLabWorkflow.DeepClone();

            this.dmxApiBroker.Setup(broker =>
                broker.GetLabWorkflowByIdAsync(inputId))
                    .ReturnsAsync(retrievedLabWorkflow);

            // when
            LabWorkflow actualLabWorkflow =
                await this.labWorkflowService.RetrieveLabWorkflowByIdAsync(inputId);

            // then
            actualLabWorkflow.Should().BeEquivalentTo(expectedLabWorkflow);

            this.dmxApiBroker.Verify(broker =>
                broker.GetLabWorkflowByIdAsync(inputId),
                    Times.Once);

            this.dmxApiBroker.VerifyNoOtherCalls();
            this.loggingBroker.VerifyNoOtherCalls();
        }
    }
}