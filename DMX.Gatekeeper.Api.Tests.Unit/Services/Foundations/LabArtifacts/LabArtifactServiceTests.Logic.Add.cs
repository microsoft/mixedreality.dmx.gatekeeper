﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
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

            // when
            await this.labArtifactService.AddLabArtifactAsync(
                    labArtifactName: inputLabArtifact.Name,
                    labArtifactContent: inputLabArtifact.Content);

            // then
            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabArtifactAsync(
                    It.Is(SameLabArtifactAs(inputLabArtifact))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
