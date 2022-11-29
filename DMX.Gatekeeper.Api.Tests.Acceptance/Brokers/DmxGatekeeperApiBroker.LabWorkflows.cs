// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.LabWorkflows;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.Brokers
{
    public partial class DmxGatekeeperApiBroker
    {
        private const string LabWorkflowsApiRelativeUrl = "api/labworkflows";

        public async ValueTask<LabWorkflow> PostLabWorkflow(LabWorkflow labWorkflow)
        {
            return await this.apiFactoryClient.PostContentAsync<LabWorkflow>(
                relativeUrl: $"{LabWorkflowsApiRelativeUrl}",
                content: labWorkflow);
        }
    }
}
