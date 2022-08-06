// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabCommands
{
    public interface ILabCommandService
    {
        ValueTask<LabCommand> AddLabCommandAsync(LabCommand labCommand);
    }
}
