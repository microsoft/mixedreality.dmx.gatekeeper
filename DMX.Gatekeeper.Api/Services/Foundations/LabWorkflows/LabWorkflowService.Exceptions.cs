// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;
using DMX.Gatekeeper.Api.Models.LabWorkflows.Exeptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowService
    {
        private delegate ValueTask<LabWorkflow> ReturningLabWorkflowFunction();

        private async ValueTask<LabWorkflow> TryCatch(
            ReturningLabWorkflowFunction returningLabWorkflowFunction)
        {
            try
            {
                return await returningLabWorkflowFunction();
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
            catch (HttpResponseInternalServerErrorException httpResponseInternalServerErrorException)
            {
                var failedLabWorkflowDependencyException =
                    new FailedLabWorkflowDependencyException(httpResponseInternalServerErrorException);

                throw CreateAndLogDependencyException(failedLabWorkflowDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabWorkflowDependencyException =
                    new FailedLabWorkflowDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedLabWorkflowDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabworkflowServiceException =
                    new FailedLabWorkflowServiceException(exception);

                throw CreateAndLogServiceException(failedLabworkflowServiceException);
            }
        }

        private LabWorkflowDependencyException CreateAndLogCriticalDependencyException(
            Xeption exception)
        {
            var labWorkflowDependencyException = new LabWorkflowDependencyException(exception);
            this.loggingBroker.LogCritical(labWorkflowDependencyException);

            return labWorkflowDependencyException;
        }

        private LabWorkflowDependencyException CreateAndLogDependencyException(
            Xeption exception)
        {
            var labWorkflowDependencyException = new LabWorkflowDependencyException(exception);
            this.loggingBroker.LogError(labWorkflowDependencyException);

            return labWorkflowDependencyException;
        }

        private LabWorkflowServiceException CreateAndLogServiceException(Xeption exception)
        {
            var labWorkflowServiceException = new LabWorkflowServiceException(exception);
            this.loggingBroker.LogError(labWorkflowServiceException);

            return labWorkflowServiceException;
        }
    }
}
