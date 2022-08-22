// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.LabCommands;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        [Fact]
        public async Task ShouldModifyLabCommandAsync()
        {
            // given
            LabCommand randomLabCommand = CreateRandomLabCommand();
            LabCommand inputLabCommand = randomLabCommand;
            LabCommand postedLabCommand = inputLabCommand;
            LabCommand expectedLabCommand = postedLabCommand.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.UpdateLabCommandAsync(inputLabCommand))
                    .ReturnsAsync(postedLabCommand);

            // when
            LabCommand actualLabCommand =
                await this.labCommandService.ModifyLabCommandAsync(
                    inputLabCommand);

            // then
            actualLabCommand.Should().BeEquivalentTo(
                expectedLabCommand);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.UpdateLabCommandAsync(inputLabCommand),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
