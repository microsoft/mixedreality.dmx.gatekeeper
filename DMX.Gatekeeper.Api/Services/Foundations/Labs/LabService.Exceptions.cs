// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.Labs;
using DMX.Gatekeeper.Api.Models.Labs.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.Labs
{
    public partial class LabService
    {
        private delegate ValueTask<Lab> AddLabsFunction();

        private delegate ValueTask<List<Lab>> ReturningLabsFunction();

        private async ValueTask<Lab> TryCatch(AddLabsFunction addLabsFunction)
        {
            try
            {
                return await addLabsFunction();
            }
            catch (NullLabException nullLabException)
            {
                throw CreateAndLogValidationException(nullLabException);
            }
        }

        private async ValueTask<List<Lab>> TryCatch(ReturningLabsFunction returningLabsFunction)
        {
            try
            {
                return await returningLabsFunction();
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(failedLabDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabDependencyException =
                    new FailedLabDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedLabDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabServiceException = new FailedLabServiceException(exception);

                throw CreateAndLogServiceException(failedLabServiceException);
            }
        }

        private LabValidationException CreateAndLogValidationException(Xeption xeption)
        {
            var labValidationException = new LabValidationException(xeption);
            this.loggingBroker.LogError(labValidationException);

            return labValidationException;
        }

        private LabDependencyException CreateAndLogCriticalDependencyException(Xeption xeption)
        {
            var labDependencyException = new LabDependencyException(xeption);
            this.loggingBroker.LogCritical(labDependencyException);

            return labDependencyException;
        }

        private LabDependencyException CreateAndLogDependencyException(Xeption xeption)
        {
            var labDependencyException = new LabDependencyException(xeption);
            this.loggingBroker.LogError(labDependencyException);

            return labDependencyException;
        }

        private LabServiceException CreateAndLogServiceException(Xeption xeption)
        {
            var labServiceException = new LabServiceException(xeption);
            this.loggingBroker.LogError(labServiceException);

            return labServiceException;
        }
    }
}
