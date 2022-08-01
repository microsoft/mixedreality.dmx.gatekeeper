// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabCommands.Exceptions
{
    public class NullLabCommandException : Xeption
    {
        public NullLabCommandException()
            : base(message: "Lab command is null.")
        { }
    }
}
