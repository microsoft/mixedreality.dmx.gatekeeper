// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.Labs;
using DMX.Gatekeeper.Api.Models.Labs.Exceptions;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xeptions;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfCriticalErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            Lab randomLab = CreateRandomLab();

            var failedLabDependencyException =
                new FailedLabDependencyException(criticalDependencyException);

            var expectedLabDependencyException =
                new LabDependencyException(failedLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<Lab> addLabTask =
                this.labService.AddLabAsync(randomLab);

            LabDependencyException actualLabDependencyException =
                await Assert.ThrowsAsync<LabDependencyException>(addLabTask.AsTask);

            // then
            actualLabDependencyException.Should()
                .BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedLabDependencyException))),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfErrorOccursAndLogItAsync()
        {
            // given
            Lab randomLab = CreateRandomLab();
            string randomMessage = GetRandomString();
            var httpMessage = new HttpResponseMessage();

            var httpResponseException = 
                new HttpResponseException(httpMessage, randomMessage);

            var failedLabDependencyException =
                new FailedLabDependencyException(httpResponseException);

            var expectedLabDependencyException =
                new LabDependencyException(failedLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<Lab> addLabTask =
                this.labService.AddLabAsync(randomLab);

            LabDependencyException actualLabDependencyException =
                await Assert.ThrowsAsync<LabDependencyException>(addLabTask.AsTask);

            //then
            actualLabDependencyException.Should()
                .BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveIfErrorOccursAndLogItAsync()
        {
            // given
            Lab randomLab = CreateRandomLab();
            var serviceException = new Exception();

            var failedlLabServiceException =
                new FailedLabServiceException(serviceException);

            var expectedLabServiceException =
                new LabServiceException(failedlLabServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Lab> addLabTask =
                this.labService.AddLabAsync(randomLab);

            var actualLabServiceException =
                await Assert.ThrowsAsync<LabServiceException>(addLabTask.AsTask);

            // then
            actualLabServiceException.Should()
                .BeEquivalentTo(expectedLabServiceException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedLabServiceException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            var randomLab = CreateRandomLab();
            var randomMessage = GetRandomString();
            HttpResponseMessage httpMessage = new HttpResponseMessage();
            var randomDictionary = CreateRandomDictionary();

            var httpBadRequestException = 
                new HttpResponseBadRequestException(httpMessage, randomMessage);

            httpBadRequestException.AddData(randomDictionary);

            var invalidPostEception = 
                new InvalidLabException(httpBadRequestException, randomDictionary);

            var expectedPostValidationDependencyException = 
                new LabDependencyValidationException(invalidPostEception);

            this.dmxApiBrokerMock.Setup(brokers =>
                brokers.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(httpBadRequestException);

            // when
            ValueTask<Lab> postLabTask =
                this.labService.AddLabAsync(randomLab);

            var actualLabDependencyValidationException =
                await Assert.ThrowsAsync<LabDependencyValidationException>(postLabTask.AsTask);

            // then
            actualLabDependencyValidationException.Should()
                .BeEquivalentTo(expectedPostValidationDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPostValidationDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfLabAlreadyExistsOccursAndLogItAsync()
        {
            // given
            Lab randomLab = CreateRandomLab();
            string randomString = GetRandomString();
            Dictionary<string, List<string>> randomDictionary = CreateRandomDictionary();
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            HttpResponseConflictException httpResponseConflictException =
                new HttpResponseConflictException(httpResponseMessage, randomString);

            httpResponseConflictException.AddData(randomDictionary);

            var alreadyExistsLabException =
                new AlreadyExistsLabException(httpResponseConflictException, randomDictionary);

            var expectedLabDependencyValidationException =
                new LabDependencyValidationException(alreadyExistsLabException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(httpResponseConflictException);

            // when
            ValueTask<Lab> addLabTask = this.labService.AddLabAsync(randomLab);

            LabDependencyValidationException actualLabDependencyValidationException =
                await Assert.ThrowsAsync<LabDependencyValidationException>(addLabTask.AsTask);

            // then
            actualLabDependencyValidationException.Should()
                .BeEquivalentTo(expectedLabDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedLabDependencyValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
