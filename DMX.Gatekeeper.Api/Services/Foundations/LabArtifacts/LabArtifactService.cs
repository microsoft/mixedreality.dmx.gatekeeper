// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using System;
using System.Threading.Tasks;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts
{
    public class LabArtifactService : ILabArtifactService
    {
        private readonly IDmxApiBroker dmxApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabArtifactService(
            IDmxApiBroker dmxApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<LabArtifact> AddArtifactAsync(LabArtifact labArtifact) =>
            await this.dmxApiBroker.PostLabArtifactAsync(labArtifact);
    }
}
