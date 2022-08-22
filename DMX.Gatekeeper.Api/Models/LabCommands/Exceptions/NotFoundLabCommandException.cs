// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabCommands.Exceptions
{
    public class NotFoundLabCommandException : Xeption
    {
        public NotFoundLabCommandException(Guid labCommandId)
            : base(message: $"Couldn't find lab command {labCommandId}")
        { }
    }
}
