// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabCommands.Exceptions
{
    public class LabCommandServiceException : Xeption
    {
        public LabCommandServiceException(Xeption innerException)
            : base(message: "Lab command service error occured, contact support",
                  innerException)
        { }
    }
}
