// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions
{
    public class InvalidLabWorkflowException : Xeption
    {
        public InvalidLabWorkflowException()
            : base(message: "Invalid lab workflow. Please fix errors and try again")
        { }

        public InvalidLabWorkflowException(Xeption innerException)
            : base(message: "Invalid lab workflow. Please fix errors and try again",
                  innerException)
        { }

        public InvalidLabWorkflowException(Exception innerException, IDictionary data)
            : base(message: "Invalid lab workflow. Please fix errors and try again",
                  innerException,
                  data)
        { }
    }
}
