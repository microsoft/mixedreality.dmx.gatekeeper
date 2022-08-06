// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.Labs.Exceptions
{
    public class LabDependencyValidationException : Xeption
    {
        public LabDependencyValidationException(Xeption innerException)
            : base(message: "Lab dependency validation error occurred. Please fix and try again.",
                  innerException)
        { }
    }
}
