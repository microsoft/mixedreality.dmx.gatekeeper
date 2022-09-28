// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;
using DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfLabWorkflowIsNullAndLogItAsync()
        {
            // given
            LabWorkflow nullLabWorkflow = null;
            var nullException = new NullLabWorkflowException();

            var expectedValidationException =
                new LabWorkflowValidationException(nullException);

            // when
            ValueTask<LabWorkflow> addLabWorkflowTask =
                this.labWorkflowService.AddLabWorkflowAsync(nullLabWorkflow);

            LabWorkflowValidationException actualValidationException =
                await Assert.ThrowsAsync<LabWorkflowValidationException>(
                    addLabWorkflowTask.AsTask);

            // then
            actualValidationException.Should().BeEquivalentTo(
                expectedValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedValidationException))),
                    Times.Once);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabWorkflowAsync(It.IsAny<LabWorkflow>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dmxApiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
