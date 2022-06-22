// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.Labs.Exceptions
{
    public class LabValidationException : Xeption
    {
        public LabValidationException(Xeption innerException)
            : base(message: "Lab validation error occurred. Please fix it and try again",
                  innerException)
        {
        }
    }
}
