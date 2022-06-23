// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.Labs.Exceptions
{
    public class InvalidLabException : Xeption
    {
        public InvalidLabException(Exception innerException) 
            : base(message: "Invalid lab error occured. Please fix and try again",
                  innerException)
        {
        }

        public InvalidLabException(Exception innerException, IDictionary data)
            : base(message: "Invalid lab error occured. Please fix and try again",
                  innerException,
                  data)
        {
        }
    }
}
