// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabArtifacts
{
    public partial class LabArtifactServiceTests
    {
        [Fact]
        public async Task ShouldAddLabArtifactAsync()
        {
            // given
            var randomLabArtifact = CreateRandomLabArtifact();
            var inputLabArtifact = randomLabArtifact;
            var returnedLabArtifact = inputLabArtifact;
            var expectedLabArtifact = inputLabArtifact.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabArtifactAsync(inputLabArtifact))
                    .ReturnsAsync(returnedLabArtifact);

            // when
            LabArtifact actualLabArtifact =
                await this.labArtifactService.AddArtifactAsync(inputLabArtifact);

            // then
            Assert.True(SameLabArtifactAs(actualLabArtifact, expectedLabArtifact));

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabArtifactAsync(inputLabArtifact),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
