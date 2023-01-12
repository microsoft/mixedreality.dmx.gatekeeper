// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions
{
    public class FailedLabArtifactServiceException : Xeption
    {
        public FailedLabArtifactServiceException(Exception exception)
            : base(message: "Failed lab artifact service error occurred. Please contact support",
                 exception)
        { }
    }
}
