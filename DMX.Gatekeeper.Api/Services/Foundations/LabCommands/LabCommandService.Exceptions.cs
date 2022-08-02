// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;
using DMX.Gatekeeper.Api.Models.LabCommands.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabCommands
{
    public partial class LabCommandService : ILabCommandService
    {
        private delegate ValueTask<LabCommand> ReturningLabCommand();

        private async ValueTask<LabCommand> TryCatch(ReturningLabCommand returningLabCommand)
        {
            try
            {
                return await returningLabCommand();
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var faliedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(faliedLabCommandDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var faliedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(faliedLabCommandDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var faliedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(faliedLabCommandDependencyException);
            }
            catch (NullLabCommandException nullLabCommandException)
            {
                throw CreateAndLogValidationException(nullLabCommandException);
            }
        }

        private LabCommandDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var labCommandDependencyException = new LabCommandDependencyException(exception);
            this.loggingBroker.LogCritical(labCommandDependencyException);

            return labCommandDependencyException;
        }

        private LabCommandValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labCommandValidationException = new LabCommandValidationException(exception);
            this.loggingBroker.LogError(labCommandValidationException);

            return labCommandValidationException;
        }
    }
}
