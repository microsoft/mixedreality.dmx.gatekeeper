// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.Commands;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask PostCommand(LabCommand labCommand);
    }
}
