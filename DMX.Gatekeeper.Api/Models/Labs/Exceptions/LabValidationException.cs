// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.Labs.Exceptions
{
    public class LabValidationException : Xeption
    {
        public LabValidationException(Xeption innerException)
            : base(message: "Lab validation errors occurred. Please try again",
                  innerException)
        {
        }
    }
}
