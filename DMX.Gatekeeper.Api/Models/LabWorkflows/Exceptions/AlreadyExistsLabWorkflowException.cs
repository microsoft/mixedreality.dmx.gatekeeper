// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions
{
    public class AlreadyExistsLabWorkflowException : Xeption
    {
        public AlreadyExistsLabWorkflowException(Exception exception)
            : base(message: "Lab workflow already exists. Please fix and try again.",
                  exception)
        { }

        public AlreadyExistsLabWorkflowException(Exception exception, IDictionary data)
            : base(message: "Lab workflow already exists. Please fix and try again.",
                  exception,
                  data)
        { }
    }
}
