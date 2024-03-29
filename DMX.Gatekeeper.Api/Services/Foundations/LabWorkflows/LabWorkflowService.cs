﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Models.LabWorkflows;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowService : ILabWorkflowService
    {
        private readonly IDmxApiBroker dmxApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabWorkflowService(
            IDmxApiBroker dmxApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<LabWorkflow> AddLabWorkflowAsync(LabWorkflow labWorkflow) =>
        TryCatch(async () =>
        {
            ValidateLabWorkflow(labWorkflow);

            return await this.dmxApiBroker.PostLabWorkflowAsync(labWorkflow);
        });

        public ValueTask<LabWorkflow> RetrieveLabWorkflowByIdAsync(Guid labWorkflowId) =>
        TryCatch(async () =>
        {
            ValidateLabWorkflowId(labWorkflowId);

            return await this.dmxApiBroker.GetLabWorkflowByIdAsync(labWorkflowId);
        });
    }
}
