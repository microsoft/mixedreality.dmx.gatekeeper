// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions
{
    public class LabArtifactDependencyException : Xeption
    {
        public LabArtifactDependencyException(Xeption innerException)
            : base(message: "Lab artifact dependency error occurred, please contact support.",
                innerException)
        { }
    }
}