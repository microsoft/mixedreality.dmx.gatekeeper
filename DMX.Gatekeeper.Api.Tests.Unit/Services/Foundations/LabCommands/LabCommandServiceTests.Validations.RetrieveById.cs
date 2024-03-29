﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;
using DMX.Gatekeeper.Api.Models.LabCommands.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionIfLabCommandIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidLabCommandId = Guid.Empty;
            var invalidLabCommandException = new InvalidLabCommandException();

            invalidLabCommandException.AddData(
                key: nameof(LabCommand.Id),
                values: "Id is required");

            var expectedLabCommandValidationException =
                new LabCommandValidationException(invalidLabCommandException);

            // when
            ValueTask<LabCommand> retrieveLabCommandByIdTask =
                this.labCommandService.RetrieveLabCommandByIdAsync(invalidLabCommandId);

            LabCommandValidationException actualLabCommandValidationException =
                await Assert.ThrowsAsync<LabCommandValidationException>(
                    retrieveLabCommandByIdTask.AsTask);

            // then
            actualLabCommandValidationException.Should().BeEquivalentTo(
                expectedLabCommandValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetLabCommandByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dmxApiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
