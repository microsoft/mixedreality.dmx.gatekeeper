// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exeptions
{
    public class LabWorkflowValidationException : Xeption
    {
        public LabWorkflowValidationException()
        {
        }

        public LabWorkflowValidationException(Exception innerException)
            : base(message: "Lab workflow validation error occurred. Please fix it and try again",
               innerException)
        {
        }
    }
}
