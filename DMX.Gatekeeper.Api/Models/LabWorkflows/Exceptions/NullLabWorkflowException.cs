// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions
{
    public class NullLabWorkflowException : Xeption
    {
        public NullLabWorkflowException()
            : base(message: "LabWorkflow is null.")
        { }
    }
}
