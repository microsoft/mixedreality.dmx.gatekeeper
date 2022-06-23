// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.Labs.Exceptions
{
    public class AlreadyExistsLabException : Xeption
    {
        public AlreadyExistsLabException(Exception exception)
            : base(message: "Lab already exists. Please fix and try again.",
                  exception)
        {
        }

        public AlreadyExistsLabException(Exception exception, IDictionary data)
            : base(message: "Lab already exists. Please fix and try again.",
                  exception,
                  data)
        {
        }
    }
}
