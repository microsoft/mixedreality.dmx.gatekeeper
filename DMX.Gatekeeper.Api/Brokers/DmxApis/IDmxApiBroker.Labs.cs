// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.Labs;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<Lab> PostLabAsync(Lab lab);
        ValueTask<List<Lab>> GetAllLabsAsync();
    }
}
