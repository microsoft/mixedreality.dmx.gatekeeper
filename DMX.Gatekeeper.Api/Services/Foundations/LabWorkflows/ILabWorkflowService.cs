// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows
{
    public interface ILabWorkflowService
    {
        ValueTask<LabWorkflow> AddLabWorkflowAsync(LabWorkflow labWorkflow);
    }
}
