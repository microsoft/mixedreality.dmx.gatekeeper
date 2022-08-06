// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

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
    }
}
