// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabWorkflow> PostLabWorkflowAsync(LabWorkflow labWorkflow);
        ValueTask<LabWorkflow> GetLabWorkflowByIdAsync(Guid id);
        ValueTask<LabWorkflow> UpdateLabWorkflowAsync(LabWorkflow labWorkflow);
        ValueTask<LabWorkflow> DeleteLabWorkflowAsync(Guid id);
    }
}
