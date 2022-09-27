// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.LabWorkflows;
using DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowService
    {
        private void ValidateLabWorkflow(LabWorkflow labWorkflow)
        {
            if (labWorkflow is null)
            {
                throw new NullLabWorkflowException();
            }
        }
    }
}
