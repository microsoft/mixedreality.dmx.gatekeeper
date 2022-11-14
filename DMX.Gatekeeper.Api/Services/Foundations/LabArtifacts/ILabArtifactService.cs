// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.LabArtifacts;
using System.Threading.Tasks;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts
{
    public interface ILabArtifactService
    {
        ValueTask<LabArtifact> AddArtifactAsync(LabArtifact labArtifact);
    }
}
