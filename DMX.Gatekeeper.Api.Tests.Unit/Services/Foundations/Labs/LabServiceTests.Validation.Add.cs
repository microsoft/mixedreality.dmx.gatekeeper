using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.Labs;
using DMX.Gatekeeper.Api.Models.Labs.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfLabIsNullAndLogItAsync()
        {
            // given
            Lab nullLab = null;

            var nullException = new NullLabException();

            var expectedValidationException = 
                new LabValidationException(nullException);

            // when
            var addLabTask = 
                this.labService.AddLabAsync(nullLab);

            var actualValidationException =
                await Assert.ThrowsAsync<LabValidationException>(addLabTask.AsTask);

            // then
            actualValidationException.Should().BeEquivalentTo(
                expectedValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedValidationException))),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
