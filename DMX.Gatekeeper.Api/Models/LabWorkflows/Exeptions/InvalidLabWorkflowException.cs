// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows.Exeptions
{
    public class InvalidLabWorkflowException : Xeption
    {
        public InvalidLabWorkflowException()
            : base(message: "Invalid Lab workflow error occurred, please contact support")
        { }

        public InvalidLabWorkflowException(Exception innerException, IDictionary data) :
            base(message: "Invalid Lab workflow error occurred, please contact support",
                innerException,
                data)
        { }
    }
}
