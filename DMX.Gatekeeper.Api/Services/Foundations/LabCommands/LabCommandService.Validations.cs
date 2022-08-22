// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using DMX.Gatekeeper.Api.Models.LabCommands;
using DMX.Gatekeeper.Api.Models.LabCommands.Exceptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabCommands
{
    public partial class LabCommandService : ILabCommandService
    {
        private static void ValidateLabCommand(LabCommand labCommand)
        {
            if (labCommand is null)
            {
                throw new NullLabCommandException();
            }
        }

        private static void ValidateLabCommandId(Guid labCommandId)
        {
            if (labCommandId == Guid.Empty)
            {
                var invalidLabCommandException =
                    new InvalidLabCommandException();

                invalidLabCommandException.AddData(
                    key: nameof(LabCommand.Id),
                    values: "Id is required");

                throw invalidLabCommandException;
            }
        }
    }
}
