// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions
{
    public class AlreadyExistsLabArtifactException : Xeption
    {
        public AlreadyExistsLabArtifactException(Exception exception)
            : base(message: "Lab artifact already exists.",
                  exception)
        { }

        public AlreadyExistsLabArtifactException(Exception exception, IDictionary data)
            : base(message: "Lab artifact already exists.",
                  exception,
                  data)
        { }
    }
}
