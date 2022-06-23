// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.Labs.Exceptions
{
    public class NullLabException : Xeption
    {
        public NullLabException()
            : base("Lab is null")
        {
        }
    }
}
