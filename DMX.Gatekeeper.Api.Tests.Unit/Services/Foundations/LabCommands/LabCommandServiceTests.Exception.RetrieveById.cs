﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;
using DMX.Gatekeeper.Api.Models.LabCommands.Exceptions;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
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
            Guid someLabCommandId = Guid.NewGuid();

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

            LabCommandDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    retrieveLabCommandByIdTask.AsTask);

            // then
            actualLabCommandDependencyException.Should().BeEquivalentTo(
                expectedLabCommandDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyException))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveIfErrorOccursAndLogItAsync(
            Exception dependencyException)
        {
            // given
            Guid someLabCommandId = Guid.NewGuid();

            var failedLabCommandDependencyException =
                new FailedLabCommandDependencyException(dependencyException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(failedLabCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyException);

            // when 
            ValueTask<LabCommand> retrieveLabCommandByIdTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(someLabCommandId);

            LabCommandDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    retrieveLabCommandByIdTask.AsTask);

            // then
            actualLabCommandDependencyException.Should().BeEquivalentTo(
                expectedLabCommandDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveIfErrorOccursAndLogItAsync()
        {
            // given
            Guid someLabCommandId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedLabCommandServiceException =
                new FailedLabCommandServiceException(serviceException);

            var expectedLabCommandServiceException =
                new LabCommandServiceException(failedLabCommandServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<LabCommand> retrieveLabCommandByIdTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(someLabCommandId);

            LabCommandServiceException actualLabCommandServiceException =
                await Assert.ThrowsAsync<LabCommandServiceException>(
                    retrieveLabCommandByIdTask.AsTask);

            // then
            actualLabCommandServiceException.Should().BeEquivalentTo(
                expectedLabCommandServiceException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandServiceException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveIfBadRequestOccursAndLogItAsync()
        {
            // given
            Guid someLabCommandId = Guid.NewGuid();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            var httpBadRequestException = new HttpResponseBadRequestException();

            httpBadRequestException.AddData(randomDictionary);

            var invalidLabCommandException =
                new InvalidLabCommandException(
                    httpBadRequestException,
                    randomDictionary);

            var expectedLabCommandDependencyValidationException =
                new LabCommandDependencyValidationException(invalidLabCommandException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpBadRequestException);

            // when
            ValueTask<LabCommand> retrieveLabCommandByIdTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(someLabCommandId);

            LabCommandDependencyValidationException
                actualLabCommandDependencyValidationException =
                    await Assert.ThrowsAsync<LabCommandDependencyValidationException>(
                        retrieveLabCommandByIdTask.AsTask);

            // then
            actualLabCommandDependencyValidationException.Should().BeEquivalentTo(
                expectedLabCommandDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveIfNotFoundErrorOccursAndLogItAsync()
        {
            // given
            Guid someLabCommandId = Guid.NewGuid();
            var httpResponseNotFoundException = new HttpResponseNotFoundException();

            var notFoundLabCommandException =
                new NotFoundLabCommandException(httpResponseNotFoundException);

            var expectedLabCommandDependencyValidationException =
                new LabCommandDependencyValidationException(notFoundLabCommandException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<LabCommand> retrieveLabCommandByIdTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(someLabCommandId);

            LabCommandDependencyValidationException
                actualLabCommandDependencyValidationException =
                    await Assert.ThrowsAsync<LabCommandDependencyValidationException>(
                        retrieveLabCommandByIdTask.AsTask);

            // then
            actualLabCommandDependencyValidationException.Should().BeEquivalentTo(
                expectedLabCommandDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
