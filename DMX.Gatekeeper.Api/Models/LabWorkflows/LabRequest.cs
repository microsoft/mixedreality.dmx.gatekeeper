// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace DMX.Gatekeeper.Api.Models.LabWorkflows
{
    public class LabRequest
    {
        public int LabCount { get; set; }
        public List<LabDesire> LabDesires { get; set; }
        public List<LabSetting> LabSettings { get; set; }
    }
}