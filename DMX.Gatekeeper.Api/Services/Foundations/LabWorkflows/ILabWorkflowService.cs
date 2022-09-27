// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.LabWorkflows;
using System.Threading.Tasks;
using System;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows
{
    public interface ILabWorkflowService
    {
        ValueTask<LabWorkflow> RetrieveLabWorkflowByIdAsync(Guid labWorkflowId);
    }
}
