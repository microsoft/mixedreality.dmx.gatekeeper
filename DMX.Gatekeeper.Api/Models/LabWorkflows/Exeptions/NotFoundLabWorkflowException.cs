// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exeptions
{
    public class NotFoundLabWorkflowException : Xeption
    {
        public NotFoundLabWorkflowException(Exception innerException)
            : base(message: "Lab workflow not found error occured, please try again.",
                  innerException)
        {
        }
    }
}
