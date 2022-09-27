// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exeptions
{
    public class LabWorkflowDependencyValidationException : Xeption
    {
        public LabWorkflowDependencyValidationException(Xeption innerException) :
            base(message: "Lab workflow dependency validation error occurred, please contact support",
                innerException)
        {
        }
    }
}
