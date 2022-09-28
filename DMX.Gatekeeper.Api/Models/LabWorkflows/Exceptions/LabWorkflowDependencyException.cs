// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions
{
    public class LabWorkflowDependencyException : Xeption
    {
        public LabWorkflowDependencyException(Xeption innerException)
            : base(message: "Lab workflow dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
