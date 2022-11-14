// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.LabArtifacts;
using System.Threading.Tasks;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabArtifact> PostLabArtifactAsync(LabArtifact labArtifact);
    }
}
