// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabWorkflowsRelativeUrl = "api/labworkflows";

        public async ValueTask<LabWorkflow> PostLabWorkflowAsync(LabWorkflow labWorkflow) =>
            await PostAsync(LabWorkflowsRelativeUrl, labWorkflow);

        public async ValueTask<LabWorkflow> GetLabWorkflowByIdAsync(Guid id) =>
            await GetAsync<LabWorkflow>($"{LabWorkflowsRelativeUrl}/{id}");

        public async ValueTask<LabWorkflow> UpdateLabWorkflowAsync(LabWorkflow labWorkflow) =>
            await UpdateAsync(LabWorkflowsRelativeUrl, labWorkflow);

        public async ValueTask<LabWorkflow> DeleteLabWorkflowAsync(Guid id) =>
            await DeleteAsync<LabWorkflow>($"{LabWorkflowsRelativeUrl}/{id}");
    }
}
