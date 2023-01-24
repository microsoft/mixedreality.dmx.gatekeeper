// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
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
                    .Throws(criticalDependencyException);

            // when
            ValueTask addLabArtifactTask =
                this.labArtifactService.AddLabArtifactAsync(
                    labArtifactName: randomLabArtifact.Name,
                    labArtifactContent: randomLabArtifact.Content);

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
                    .Throws(dependencyException);

            // when
            ValueTask addLabArtifactTask =
                this.labArtifactService.AddLabArtifactAsync(
                    labArtifactName: randomLabArtifact.Name,
                    labArtifactContent: randomLabArtifact.Content);

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

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfErrorOccursAndLogItAsync()
        {
            // given
            LabArtifact randomLabArtifact = CreateRandomLabArtifact();
            var serviceException = new Exception();

            var failedLabArtifactServiceException =
                new FailedLabArtifactServiceException(serviceException);

            var expectedLabArtifactServiceException =
                new LabArtifactServiceException(failedLabArtifactServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabArtifactAsync(It.IsAny<LabArtifact>()))
                    .Throws(serviceException);

            // when
            ValueTask addLabArtifactTask =
                this.labArtifactService.AddLabArtifactAsync(
                    labArtifactName: randomLabArtifact.Name,
                    labArtifactContent: randomLabArtifact.Content);

            LabArtifactServiceException actualLabArtifactServiceException =
                await Assert.ThrowsAsync<LabArtifactServiceException>(
                    addLabArtifactTask.AsTask);

            // then
            actualLabArtifactServiceException.Should()
                .BeEquivalentTo(expectedLabArtifactServiceException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabArtifactAsync(It.IsAny<LabArtifact>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabArtifactServiceException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            LabArtifact randomLabArtifact = CreateRandomLabArtifact();
            string randomMessage = GetRandomString();
            var httpMessage = new HttpResponseMessage();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            var httpBadRequestException =
                new HttpResponseBadRequestException(
                    httpMessage,
                    randomMessage);

            httpBadRequestException.AddData(randomDictionary);

            var invalidLabArtifactException =
                new InvalidLabArtifactException(
                    httpBadRequestException,
                    randomDictionary);

            var expectedLabArtifactDependencyValidationException =
                new LabArtifactDependencyValidationException(invalidLabArtifactException);

            this.dmxApiBrokerMock.Setup(brokers =>
                brokers.PostLabArtifactAsync(It.IsAny<LabArtifact>()))
                    .Throws(httpBadRequestException);

            // when
            ValueTask addLabArtifactTask =
                this.labArtifactService.AddLabArtifactAsync(
                    labArtifactName: randomLabArtifact.Name,
                    labArtifactContent: randomLabArtifact.Content);

            LabArtifactDependencyValidationException actualLabArtifactDependencyValidationException =
                await Assert.ThrowsAsync<LabArtifactDependencyValidationException>(
                    addLabArtifactTask.AsTask);

            // then
            actualLabArtifactDependencyValidationException.Should()
                .BeEquivalentTo(expectedLabArtifactDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabArtifactAsync(
                    It.IsAny<LabArtifact>()),
                        Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabArtifactDependencyValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfLabArtifactAlreadyExistsErrorOccursAndLogItAsync()
        {
            // given
            LabArtifact randomLabArtifact = CreateRandomLabArtifact();
            var httpResponseConflictException = new HttpResponseConflictException();

            var alreadyExistsLabArtifactException =
                new AlreadyExistsLabArtifactException(httpResponseConflictException);

            var expectedDependencyValidationException =
                new LabArtifactDependencyValidationException(alreadyExistsLabArtifactException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabArtifactAsync(It.IsAny<LabArtifact>()))
                    .Throws(httpResponseConflictException);

            // when
            ValueTask addLabArtifactTask =
                this.labArtifactService.AddLabArtifactAsync(
                    labArtifactName: randomLabArtifact.Name,
                    labArtifactContent: randomLabArtifact.Content);

            LabArtifactDependencyValidationException actualDependencyValidationException =
                await Assert.ThrowsAsync<LabArtifactDependencyValidationException>(
                    addLabArtifactTask.AsTask);

            // then
            actualDependencyValidationException.Should()
                .BeEquivalentTo(expectedDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabArtifactAsync(
                    It.IsAny<LabArtifact>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
