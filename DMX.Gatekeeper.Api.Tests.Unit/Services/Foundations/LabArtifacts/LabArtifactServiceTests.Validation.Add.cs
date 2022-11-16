// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabArtifacts
{
    public partial class LabArtifactServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfLabArtifactIsNullAndLogItAsync()
        {
            // given
            LabArtifact nullLabArtifact = null;

            var nullLabArtifactException =
                new NullLabArtifactException();

            var expectedLabArtifactValidationException =
                new LabArtifactValidationException(nullLabArtifactException);

            // when
            ValueTask<LabArtifact> addLabArtifactTask =
                this.labArtifactService.AddArtifactAsync(nullLabArtifact);

            LabArtifactValidationException actualLabArtifactValidationException =
                await Assert.ThrowsAsync<LabArtifactValidationException>(
                    addLabArtifactTask.AsTask);

            // then
            actualLabArtifactValidationException.Should().BeEquivalentTo(
                expectedLabArtifactValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabArtifactValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabArtifactAsync(It.IsAny<LabArtifact>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dmxApiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
