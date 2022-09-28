// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.LabWorkflows;
using DMX.Gatekeeper.Api.Models.LabWorkflows.Exeptions;
using System;
using System.Data;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowService
    {

        private static void ValidateLabWorkflowId(Guid labWorkflowId)
        {
            Validate(
                (Rule: IsInvalid(labWorkflowId), Parameter: nameof(LabWorkflow.Id)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private void ValidateLabWorkflow(LabWorkflow labWorkflow)
        {
            if (labWorkflow is null)
                throw new NullLabWorkflowException();
            }
        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidLabWorkflowException = new InvalidLabWorkflowException();

            foreach(var (rule, parameter) in validations)
                if (rule.Condition)
                    invalidLabWorkflowException.AddData(
                        key: parameter,
                        values: rule.Message);
                }

            invalidLabWorkflowException.ThrowIfContainsErrors();
        }
    }
}
