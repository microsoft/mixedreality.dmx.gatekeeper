// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions;
using Xeptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts
{
    public partial class LabArtifactService : ILabArtifactService
    {
        private delegate ValueTask<LabArtifact> ReturningLabArtifactFunction();

        private async ValueTask<LabArtifact> TryCatch(ReturningLabArtifactFunction returningLabArtifactFunction)
        {
            try
            {
                return await returningLabArtifactFunction();
            }
            catch (NullLabArtifactException nullLabArtifactException)
            {
                throw CreateAndLogValidationException(nullLabArtifactException);
            }
        }

        private LabArtifactValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labArtifactValidationException = new LabArtifactValidationException(exception);
            this.loggingBroker.LogError(labArtifactValidationException);

            return labArtifactValidationException;
        }
    }
}