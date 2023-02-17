// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows
{
    public class LabWorkflowExecution
    {
        public Guid Id { get; set; }
        public List<LabRequest> LabRequests { get; set; }
        public LabWorkflow LabWorkflow { get; set; }
    }
}
