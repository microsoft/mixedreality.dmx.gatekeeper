// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
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
        public async Task ShouldRetrieveLabCommandByIdAsync()
        {
            // given
            Guid randomLabCommandId = Guid.NewGuid();
            Guid inputLabCommandId = randomLabCommandId;
            LabCommand randomLabCommand = CreateRandomLabCommand();
            LabCommand retrievedLabCommand = randomLabCommand.DeepClone();
            LabCommand expectedLabCommand = retrievedLabCommand;

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetLabCommandByIdAsync(inputLabCommandId))
                    .ReturnsAsync(retrievedLabCommand);

            // when
            LabCommand actualLabCommand =
                await this.labCommandService.RetrieveLabCommandByIdAsync(
                    inputLabCommandId);

            // then
            actualLabCommand.Should().BeEquivalentTo(expectedLabCommand);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(inputLabCommandId),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
