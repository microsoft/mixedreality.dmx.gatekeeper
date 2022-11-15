// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions;
using RESTFulSense.Exceptions;
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
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabArtifactDependencyException =
                    new FailedLabArtifactDependencyException(
                        httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(
                    failedLabArtifactDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedLabArtifactDependencyException =
                    new FailedLabArtifactDependencyException(
                        httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(
                    failedLabArtifactDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabArtifactDependencyException =
                    new FailedLabArtifactDependencyException(
                        httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(
                    failedLabArtifactDependencyException);
            }
            catch (HttpResponseInternalServerErrorException httpResponseInternalServerErrorException)
            {
                var failedLabArtifactDependencyException =
                    new FailedLabArtifactDependencyException(httpResponseInternalServerErrorException);

                throw CreateAndLogDependencyException(failedLabArtifactDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabArtifactDependencyException =
                    new FailedLabArtifactDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedLabArtifactDependencyException);
            }
        }

        private LabArtifactValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labArtifactValidationException = new LabArtifactValidationException(exception);
            this.loggingBroker.LogError(labArtifactValidationException);

            return labArtifactValidationException;
        }

        private LabArtifactDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var labArtifactDependencyException = new LabArtifactDependencyException(exception);
            this.loggingBroker.LogCritical(labArtifactDependencyException);

            return labArtifactDependencyException;
        }

        private LabArtifactDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var labArtifactDependencyException = new LabArtifactDependencyException(exception);
            this.loggingBroker.LogError(labArtifactDependencyException);

            return labArtifactDependencyException;
        }
    }
}