// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions
{
    public class LabArtifactDependencyValidationException : Xeption
    {
        public LabArtifactDependencyValidationException(Xeption innerException)
            : base(message: "Lab artifact dependency validation error occurred. Please fix and try again.",
                  innerException)
        { }
    }
}
