// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabCommandsRelativeUrl = "api/labcommands";

        public async ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand) =>
            await PostAsync(LabCommandsRelativeUrl, labCommand);
        
        public async ValueTask<LabCommand> GetLabCommandByIdAsync(Guid id) =>
            await GetAsync<LabCommand>($"{LabCommandsRelativeUrl}/{id}");

        public async ValueTask<LabCommand> UpdateLabCommandAsync(LabCommand labCommand) =>
            await UpdateAsync(LabCommandsRelativeUrl, labCommand);
            
        public async ValueTask<LabCommand> DeleteLabCommandAsync(Guid id) =>
            await DeleteAsync<LabCommand>($"{LabCommandsRelativeUrl}/{id}");
    }
}
