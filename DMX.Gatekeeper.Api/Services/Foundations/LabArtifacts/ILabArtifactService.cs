// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabArtifacts;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts
{
    public interface ILabArtifactService
    {
        ValueTask<LabArtifact> AddArtifactAsync(LabArtifact labArtifact);
    }
}
