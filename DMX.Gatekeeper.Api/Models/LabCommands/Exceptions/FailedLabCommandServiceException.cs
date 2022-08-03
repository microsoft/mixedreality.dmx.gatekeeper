// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabCommands.Exceptions
{
    public class FailedLabCommandServiceException : Xeption
    {
        public FailedLabCommandServiceException(Exception innerException)
            : base(message: "Failed lab command error occured, contact support.",
                  innerException)
        { }
    }
}
