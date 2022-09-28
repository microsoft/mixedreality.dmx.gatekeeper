// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions
{
    public class LabWorkflowDependencyValidationException : Xeption
    {
        public LabWorkflowDependencyValidationException(Xeption innerException)
            : base(message: "Lab workflow dependency validation error occurred. Please fix errors and try again.",
                   innerException)
        { }
    }
}
