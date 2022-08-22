// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Models.LabCommands;

namespace DMX.Gatekeeper.Api.Services.Foundations.LabCommands
{
    public partial class LabCommandService : ILabCommandService
    {
        private readonly IDmxApiBroker dmxApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabCommandService(
            IDmxApiBroker dmxApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<LabCommand> AddLabCommandAsync(LabCommand labCommand) =>
        TryCatch(async () =>
        {
            ValidateLabCommand(labCommand);

            return await this.dmxApiBroker.PostLabCommandAsync(labCommand);
        });

        public async ValueTask<LabCommand> ModifyLabCommandAsync(LabCommand labCommand) =>
            await this.dmxApiBroker.UpdateLabCommandAsync(labCommand);
    }
}
