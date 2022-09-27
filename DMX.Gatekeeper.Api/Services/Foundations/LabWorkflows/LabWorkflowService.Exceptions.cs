// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;
using DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions;
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

        }

        private LabWorkflowValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labWorkflowValidationException = new LabWorkflowValidationException(exception);
            this.loggingBroker.LogError(labWorkflowValidationException);

            return labWorkflowValidationException;
        }
    }
}
