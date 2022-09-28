// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions
{
    public class FailedLabWorkflowDependencyException : Xeption
    {
        public FailedLabWorkflowDependencyException(Exception innerException)
            : base(message: "Failed lab workflow dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
