// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabArtifacts
{
    public partial class LabArtifactServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfCriticalErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            LabArtifact randomLabArtifact = CreateRandomLabArtifact();

            var failedLabArtifactDependencyException =
                new FailedLabArtifactDependencyException(criticalDependencyException);

            var expectedLabArtifactDependencyException =
                new LabArtifactDependencyException(failedLabArtifactDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabArtifactAsync(It.IsAny<LabArtifact>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<LabArtifact> addLabArtifactTask =
                this.labArtifactService.AddArtifactAsync(randomLabArtifact);

            LabArtifactDependencyException actualLabArtifactDependencyException =
                await Assert.ThrowsAsync<LabArtifactDependencyException>(
                    addLabArtifactTask.AsTask);

            // then
            actualLabArtifactDependencyException.Should().BeEquivalentTo(
                expectedLabArtifactDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabArtifactAsync(It.IsAny<LabArtifact>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabArtifactDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyException))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            LabArtifact randomLabArtifact = CreateRandomLabArtifact();

            var failedLabArtifactDependencyException =
                new FailedLabArtifactDependencyException(dependencyException);

            var expectedLabArtifactDependencyException =
                new LabArtifactDependencyException(failedLabArtifactDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabArtifactAsync(It.IsAny<LabArtifact>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<LabArtifact> addLabArtifactTask =
                this.labArtifactService.AddArtifactAsync(randomLabArtifact);

            LabArtifactDependencyException actualLabArtifactDependencyException =
                await Assert.ThrowsAsync<LabArtifactDependencyException>(
                    addLabArtifactTask.AsTask);

            // then
            actualLabArtifactDependencyException.Should().BeEquivalentTo(
                expectedLabArtifactDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabArtifactAsync(It.IsAny<LabArtifact>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabArtifactDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
