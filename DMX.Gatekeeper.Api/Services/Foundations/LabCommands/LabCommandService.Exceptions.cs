// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;
using DMX.Gatekeeper.Api.Models.LabCommands.Exceptions;
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
            catch (NullLabCommandException nullLabCommandException)
            {
                throw CreateAndLogValidationException(nullLabCommandException);
            }
        }

        private LabCommandValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labCommandValidationException = new LabCommandValidationException(exception);
            this.loggingBroker.LogError(labCommandValidationException);

            return labCommandValidationException;
        }
    }
}
