// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabArtifacts;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabArtifactsRelativeUrl = "api/labartifacts";
        private const string LabArtifactsMediaType = "application/octet-stream";

        public async ValueTask<LabArtifact> PostLabArtifactAsync(LabArtifact labArtifact) =>
            await PostAsync(
                $"{LabArtifactsRelativeUrl}?streamName={labArtifact.Name}",
                labArtifact,
                LabArtifactsMediaType);
    }
}
