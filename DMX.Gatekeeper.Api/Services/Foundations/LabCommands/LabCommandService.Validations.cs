// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using DMX.Gatekeeper.Api.Models.LabCommands;
using DMX.Gatekeeper.Api.Models.LabCommands.Exceptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabCommands
{
    public partial class LabCommandService : ILabCommandService
    {
        private static void ValidateLabCommand(LabCommand labCommand)
        {
            if (labCommand is null)
            {
                throw new NullLabCommandException();
            }
        }

        private static void ValidateLabCommandId(Guid labCommandId)
        {
            Validate(
                (Rule: IsInvalid(labCommandId), Parameter: (nameof(LabCommand.Id))));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static void Validate(params (dynamic Rule ,string Parameter)[] validations)
        {
            var invalidLabCommandException = new InvalidLabCommandException();

            foreach (var (rule, parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidLabCommandException.AddData(
                        key: parameter,
                        values: rule.Message);
                }
            }

            invalidLabCommandException.ThrowIfContainsErrors();
        }
    }
}
