// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabCommands.Exceptions
{
    public class LabCommandDependencyValidationException : Xeption
    {
        public LabCommandDependencyValidationException(Xeption innerException)
            : base(message: "Lab command dependency validation error occured. Please fix and try again.",
                  innerException)
        { }
    }
}
