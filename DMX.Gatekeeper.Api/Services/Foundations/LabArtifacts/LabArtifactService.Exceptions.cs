// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts
{
    public partial class LabArtifactService : ILabArtifactService
    {
        private delegate ValueTask ReturningLabArtifactFunction();

        private async ValueTask TryCatch(ReturningLabArtifactFunction returningLabArtifactFunction)
        {
            try
            {
                await returningLabArtifactFunction();
            }
            catch (InvalidLabArtifactException invalidLabArtifactException)
            {
                throw CreateAndLogValidationException(invalidLabArtifactException);
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
                    new FailedLabArtifactDependencyException(
                        httpResponseInternalServerErrorException);

                throw CreateAndLogDependencyException(
                    failedLabArtifactDependencyException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidLabArtifactException =
                    new InvalidLabArtifactException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidLabArtifactException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsLabArtifactException =
                    new AlreadyExistsLabArtifactException(
                        httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(
                    alreadyExistsLabArtifactException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabArtifactDependencyException =
                    new FailedLabArtifactDependencyException(
                        httpResponseException);

                throw CreateAndLogDependencyException(
                    failedLabArtifactDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabArtifactServiceException =
                    new FailedLabArtifactServiceException(
                        exception);

                throw CreateAndLogServiceException(
                    failedLabArtifactServiceException);
            }
        }

        private LabArtifactDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var labArtifactDependencyValidationException =
                new LabArtifactDependencyValidationException(exception);

            this.loggingBroker.LogError(labArtifactDependencyValidationException);

            return labArtifactDependencyValidationException;
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

        private LabArtifactServiceException CreateAndLogServiceException(Xeption exception)
        {
            var labArtifactServiceException = new LabArtifactServiceException(exception);
            this.loggingBroker.LogError(labArtifactServiceException);

            return labArtifactServiceException;
        }
    }
}