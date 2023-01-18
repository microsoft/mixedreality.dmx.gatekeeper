// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Models.LabArtifacts;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts
{
    public partial class LabArtifactService : ILabArtifactService
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

        public ValueTask AddLabArtifactAsync(string labArtifactName, Stream labArtifactContent) =>
        TryCatch(async () =>
        {
            ValidateLabArtifactPropertiesOnAdd(labArtifactName, labArtifactContent);
            
            var labArtifact = new LabArtifact
            {
                Name = labArtifactName,
                Content = labArtifactContent
            };
            
            await this.dmxApiBroker.PostLabArtifactAsync(labArtifact);
        });
    }
}
