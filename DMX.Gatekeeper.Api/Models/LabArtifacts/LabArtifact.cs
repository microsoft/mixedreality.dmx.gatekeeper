// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.IO;

namespace DMX.Gatekeeper.Api.Models.LabArtifacts
{
    public class LabArtifact
    {
        public string Name { get; set; }
        public MemoryStream Content { get; set; }
    }
}
