// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask PostArtifactAsync(Stream stream);
    }
}