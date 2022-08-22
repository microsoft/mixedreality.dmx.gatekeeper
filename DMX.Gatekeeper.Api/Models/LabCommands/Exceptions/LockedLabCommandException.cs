// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabCommands.Exceptions
{
    public class LockedLabCommandException : Xeption
    {
        public LockedLabCommandException(Exception innerException)
            : base(message: "Lab command locked error occured. Please try again.", innerException)
        { }
    }
}
