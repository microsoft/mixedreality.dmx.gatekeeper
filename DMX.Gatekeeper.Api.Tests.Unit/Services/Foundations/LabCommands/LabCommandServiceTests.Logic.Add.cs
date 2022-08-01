// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        [Fact]
        public async Task ShouldAddLabCommandAsync()
        {
            // given
            LabCommand randomLabCommand = CreateRandomLabCommand();
            LabCommand inputLabCommand = randomLabCommand;
            LabCommand postedLabCommand = inputLabCommand;
            LabCommand expectedLabCommand = postedLabCommand.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabCommandAsync(inputLabCommand))
                    .ReturnsAsync(postedLabCommand);

            // when
            LabCommand actualLabCommand =
                await this.labCommandService.AddLabCommandAsync(
                    inputLabCommand);

            // then
            actualLabCommand.Should().BeEquivalentTo(expectedLabCommand);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(inputLabCommand),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
