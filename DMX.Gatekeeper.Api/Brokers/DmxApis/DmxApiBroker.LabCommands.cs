// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabCommandsRelativeUrl = "api/labcommands";

        public async ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand) =>
            await PostAsync(LabCommandsRelativeUrl, labCommand);
    }
}
