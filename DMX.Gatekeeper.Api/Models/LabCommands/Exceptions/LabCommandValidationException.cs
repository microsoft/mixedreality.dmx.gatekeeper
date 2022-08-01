// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabCommands.Exceptions
{
    public class LabCommandValidationException : Xeption
    {
        public LabCommandValidationException(Xeption innerException)
            : base(message: "Lab command validation exception occured. Please fix and try again.", innerException)
        { }
    }
}
