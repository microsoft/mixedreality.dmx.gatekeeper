// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions
{
    public class LabArtifactServiceException : Xeption
    {
        public LabArtifactServiceException(Xeption exception)
            : base(message: "Lab artifact service error occurred, please contact support.",
                 exception)
        { }
    }
}
