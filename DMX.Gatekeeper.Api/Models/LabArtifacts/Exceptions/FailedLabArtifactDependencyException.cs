// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions
{
    public class FailedLabArtifactDependencyException : Xeption
    {
        public FailedLabArtifactDependencyException(Xeption innerException)
            : base(message: "Failed lab artifact dependency error occurred, please contact support.",
                innerException)
        { }
    }
}