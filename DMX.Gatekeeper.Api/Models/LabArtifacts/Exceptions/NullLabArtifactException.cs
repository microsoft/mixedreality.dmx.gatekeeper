// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions
{
    public class NullLabArtifactException : Xeption
    {
        public NullLabArtifactException()
            : base(message: "Lab artifact is null.")
        { }
    }
}
