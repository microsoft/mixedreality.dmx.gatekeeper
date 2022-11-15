// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts
{
    public partial class LabArtifactService : ILabArtifactService
    {
        private void ValidateLabArtifact(LabArtifact labArtifact)
        {
            if (labArtifact is null)
            {
                throw new NullLabArtifactException();
            }
        }
    }
}