// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.Labs.Exceptions
{
    public class FailedLabDependencyException : Xeption
    {
        public FailedLabDependencyException(Exception innerException)
            : base(message: "Failed lab dependency error occured, contact support.",
                  innerException)
        { }
    }
}
