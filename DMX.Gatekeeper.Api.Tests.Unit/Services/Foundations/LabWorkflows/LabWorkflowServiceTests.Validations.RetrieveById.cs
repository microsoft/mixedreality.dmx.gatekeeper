// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;
using DMX.Gatekeeper.Api.Models.LabWorkflows.Exeptions;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using NuGet.Frameworks;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfLabWorkflowIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidId = Guid.Empty;
            var invalidLabWorkflowException = new InvalidLabWorkflowException();

            invalidLabWorkflowException.AddData(
                key: nameof(LabWorkflow.Id),
                values: "Id is required");

            var expectedLabWorkflowValidationException =
                new LabWorkflowValidationException(invalidLabWorkflowException);

            // when
            ValueTask<LabWorkflow> retrieveLabWorkflowByIdTask =
                this.labWorkflowService.RetrieveLabWorkflowByIdAsync(invalidId);

            LabWorkflowValidationException actualLabWorkflowValidationException =
                await Assert.ThrowsAsync<LabWorkflowValidationException>(
                    retrieveLabWorkflowByIdTask.AsTask);

            // then
            actualLabWorkflowValidationException.Should().BeEquivalentTo(
                expectedLabWorkflowValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabWorkflowValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.Verify(broker =>
               broker.GetLabWorkflowByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
