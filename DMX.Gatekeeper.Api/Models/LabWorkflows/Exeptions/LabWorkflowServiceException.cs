// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exeptions
{
    public class LabWorkflowServiceException : Xeption
    {
        public LabWorkflowServiceException(Xeption innerException)
            : base(message: "Lab workflow error occured. Please contact support.",
                  innerException)
        {
        }
    }
}
