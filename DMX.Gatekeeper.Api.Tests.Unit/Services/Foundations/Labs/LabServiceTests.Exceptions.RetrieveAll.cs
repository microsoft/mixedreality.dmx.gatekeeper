// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.Labs;
using DMX.Gatekeeper.Api.Models.Labs.Exceptions;
using Moq;
using RESTFulSense.Exceptions;
using Xeptions;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrievalIfCriticalErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            var failedExternalLabDependencyException =
                new FailedLabDependencyException(criticalDependencyException);

            var expectedLabDependencyException =
                new LabDependencyException(failedExternalLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Lab>> getAllLabsTask =
                this.labService.RetrieveAllLabsAsync();

            // then
            await Assert.ThrowsAsync<LabDependencyException>(() =>
                getAllLabsTask.AsTask());

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetAllLabsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrievalIfErrorOccursAndLogItAsync()
        {
            // given
            var someMessage = GetRandomString();
            var someResponseMessage = new HttpResponseMessage();
            var httpResponseException = new HttpResponseException(someResponseMessage, someMessage);

            var failedExternalLabDependencyException =
                new FailedLabDependencyException(httpResponseException);

            var expectedLabDependencyException =
                new LabDependencyException(failedExternalLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<List<Lab>> getAllLabsTask =
                this.labService.RetrieveAllLabsAsync();

            // then
            await Assert.ThrowsAsync<LabDependencyException>(() =>
                getAllLabsTask.AsTask());

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetAllLabsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
