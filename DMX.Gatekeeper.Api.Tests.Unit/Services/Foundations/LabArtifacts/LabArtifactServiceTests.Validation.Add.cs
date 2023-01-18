// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.IO;
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
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfArtifactIsInvalidAndLogItAsync(
            string invalidString)
        {
            // given
            string invalidLabArtifactName = invalidString;
            Stream invalidLabArtifactContent = null;

            var invalidArtifactException = new InvalidLabArtifactException();

            invalidArtifactException.AddData(
                key: nameof(LabArtifact.Name),
                values: "Text is required");

            invalidArtifactException.AddData(
                key: nameof(LabArtifact.Content),
                values: "Content is required");

            var expectedArtifactValidationException =
                new LabArtifactValidationException(invalidArtifactException);

            // when
            ValueTask addArtifactTask =
                this.labArtifactService.AddLabArtifactAsync(
                    invalidLabArtifactName,
                    invalidLabArtifactContent);

            LabArtifactValidationException actualArtifactValidationException =
                await Assert.ThrowsAsync<LabArtifactValidationException>(
                    addArtifactTask.AsTask);

            // then
            actualArtifactValidationException.Should()
                .BeEquivalentTo(expectedArtifactValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedArtifactValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabArtifactAsync(
                    It.IsAny<LabArtifact>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dmxApiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
