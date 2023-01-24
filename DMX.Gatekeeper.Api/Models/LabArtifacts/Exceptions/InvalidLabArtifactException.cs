// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions
{
    public class InvalidLabArtifactException : Xeption
    {
        public InvalidLabArtifactException()
            : base(message: "Invalid lab artifact error occured. Please fix and try again")
        { }


        public InvalidLabArtifactException(Exception innerException)
            : base(message: "Invalid lab artifact error occured. Please fix and try again",
                  innerException)
        { }

        public InvalidLabArtifactException(Exception innerException, IDictionary data)
            : base(message: "Invalid lab artifact error occured. Please fix and try again",
                  innerException,
                  data)
        { }
    }
}
