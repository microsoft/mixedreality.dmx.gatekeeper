// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace DMX.Gatekeeper.Api.Models.Configurations
{
    public class DownstreamApiConfiguration
    {
        public string BaseUrl { get; set; }
        public Dictionary<string, string> Scopes { get; set; }
    }
}
