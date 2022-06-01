// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.Labs;

namespace DMX.Gatekeeper.Api.Services.Foundations.Labs
{
    public interface ILabService
    {
        ValueTask<List<Lab>> RetrieveAllLabsAsync();
    }
}
