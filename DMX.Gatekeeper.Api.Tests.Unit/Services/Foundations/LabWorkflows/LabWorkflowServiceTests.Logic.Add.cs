// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

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
        public async Task ShouldAddLabWorkflowAsync()
        {
            // given
            LabWorkflow randomLabWorkflow = CreateRandomLabWorkflow();
            LabWorkflow inputLabWorkflow = randomLabWorkflow;
            LabWorkflow postedLabWorkflow = inputLabWorkflow;
            LabWorkflow expectedLabWorkflow = postedLabWorkflow.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabWorkflowAsync(inputLabWorkflow))
                    .ReturnsAsync(postedLabWorkflow);

            // when
            LabWorkflow actualLabWorkflow =
                await this.labWorkflowService.AddLabWorkflowAsync(inputLabWorkflow);

            // then
            actualLabWorkflow.Should().BeEquivalentTo(expectedLabWorkflow);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabWorkflowAsync(inputLabWorkflow),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
