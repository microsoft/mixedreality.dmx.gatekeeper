using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;
using DMX.Gatekeeper.Api.Models.LabCommands.Exceptions;
using DMX.Gatekeeper.Api.Models.Labs.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveIfCriticalErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            var someLabCommandId = Guid.NewGuid();

            var failedLabCommandDependencyException =
                new FailedLabCommandDependencyException(criticalDependencyException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(failedLabCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(criticalDependencyException);

            // when 
            ValueTask<LabCommand> retrieveLabCommandByIdTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(someLabCommandId);

            LabDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabDependencyException>(async () =>
                    await retrieveLabCommandByIdTask);

            // then
            actualLabCommandDependencyException.Should().BeEquivalentTo(
                expectedLabCommandDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(someLabCommandId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyException))),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.dmxApiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
