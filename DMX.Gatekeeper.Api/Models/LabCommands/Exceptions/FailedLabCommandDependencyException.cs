// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabCommands.Exceptions
{
    public class FailedLabCommandDependencyException : Xeption
    {
        public FailedLabCommandDependencyException(Exception innerException)
            : base(message: "Failed lab command error occurred, contact support.",
                  innerException)
        { }
    }
}
