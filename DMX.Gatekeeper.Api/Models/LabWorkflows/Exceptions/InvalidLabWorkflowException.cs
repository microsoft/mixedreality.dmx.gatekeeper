// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions
{
    public class InvalidLabWorkflowException : Xeption
    {
        public InvalidLabWorkflowException(Xeption innerException)
            : base(message: "Invalid lab workflow, please fix errors and try again",
                  innerException)
        { }
    }
}
