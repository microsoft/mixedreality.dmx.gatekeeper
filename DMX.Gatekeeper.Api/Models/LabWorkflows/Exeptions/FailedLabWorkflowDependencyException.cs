// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exeptions
{
    public class FailedLabWorkflowDependencyException : Xeption
    {
        public FailedLabWorkflowDependencyException(Exception innerException)
            : base(message: "Failed lab workflow error occured. Please contact support.",
                  innerException)
        {
        }
    }
}
