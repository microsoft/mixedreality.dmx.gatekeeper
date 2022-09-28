// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions
{
    public class LabWorkflowValidationException : Xeption
    {
        public LabWorkflowValidationException(Xeption innerException)
            : base(message: "Lab workflow validation error occurred, fix errors and try again.",
                   innerException)
        { }
    }
}
