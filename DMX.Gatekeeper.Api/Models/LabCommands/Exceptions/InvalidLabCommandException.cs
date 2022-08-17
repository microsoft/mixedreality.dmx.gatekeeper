// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabCommands.Exceptions
{
    public class InvalidLabCommandException : Xeption
    {
        public InvalidLabCommandException(Exception innerException)
            : base(message: "Invalid lab command exception occured. Please fix and try again.",
                  innerException)
        { }

        public InvalidLabCommandException(Exception innerException, IDictionary data)
            : base(message: "Invalid lab command exception occured. Please fix and try again.",
                  innerException,
                  data)
        { }
    }
}
