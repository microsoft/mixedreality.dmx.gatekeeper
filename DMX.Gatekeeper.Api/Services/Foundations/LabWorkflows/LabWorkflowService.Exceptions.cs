// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;
using DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowService
    {
        private delegate ValueTask<LabWorkflow> ReturningLabWorkflowFunction();

        private async ValueTask<LabWorkflow> TryCatch(ReturningLabWorkflowFunction returningLabWorkflowFunction)
        {
            try
            {
                return await returningLabWorkflowFunction();
            }
            catch (NullLabWorkflowException nullLabWorkflowException)
            {
                throw CreateAndLogValidationException(nullLabWorkflowException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabWorkflowDependencyException =
                    new FailedLabWorkflowDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabWorkflowDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedLabWorkflowDependencyException =
                    new FailedLabWorkflowDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedLabWorkflowDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabWorkflowDependencyException =
                    new FailedLabWorkflowDependencyException(httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(failedLabWorkflowDependencyException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidLabWorkflowException =
                    new InvalidLabWorkflowException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidLabWorkflowException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidLabWorkflowException =
                    new AlreadyExistsLabWorkflowException(
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidLabWorkflowException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabWorkflowDependencyException =
                    new FailedLabWorkflowDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedLabWorkflowDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabWorkflowServiceException =
                    new FailedLabWorkflowServiceException(exception);

                throw CreateAndLogServiceException(failedLabWorkflowServiceException);
            }
        }

        private LabWorkflowValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labWorkflowValidationException = new LabWorkflowValidationException(exception);
            this.loggingBroker.LogError(labWorkflowValidationException);

            return labWorkflowValidationException;
        }

        private LabWorkflowDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var labWorkflowDependencyException =
                new LabWorkflowDependencyException(exception);

            this.loggingBroker.LogCritical(labWorkflowDependencyException);

            return labWorkflowDependencyException;
        }

        private LabWorkflowDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var labWorkflowDependencyException =
                new LabWorkflowDependencyException(exception);

            this.loggingBroker.LogError(labWorkflowDependencyException);

            return labWorkflowDependencyException;
        }

        private LabWorkflowDependencyValidationException CreateAndLogDependencyValidationException(Xeption xeption)
        {
            var labWorkflowDependencyValidationException = new LabWorkflowDependencyValidationException(xeption);
            this.loggingBroker.LogError(labWorkflowDependencyValidationException);

            return labWorkflowDependencyValidationException;
        }

        private LabWorkflowServiceException CreateAndLogServiceException(Xeption exception)
        {
            var labWorkflowServiceException =
                new LabWorkflowServiceException(exception);

            this.loggingBroker.LogError(labWorkflowServiceException);

            return labWorkflowServiceException;
        }
    }
}
