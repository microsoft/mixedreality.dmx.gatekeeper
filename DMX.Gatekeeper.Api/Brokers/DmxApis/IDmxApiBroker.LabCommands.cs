﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand);
        ValueTask<LabCommand> GetLabCommandByIdAsync(Guid id);
        ValueTask<LabCommand> UpdateLabCommandAsync(LabCommand labCommand);
        ValueTask<LabCommand> DeleteLabCommandAsync(Guid id);
    }
}
