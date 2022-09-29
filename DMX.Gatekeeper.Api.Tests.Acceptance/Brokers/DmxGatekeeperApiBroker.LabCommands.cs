// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.LabCommands;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.Brokers
{
    public partial class DmxGatekeeperApiBroker
    {
        private const string LabCommandsApiRelativeUrl = "api/labcommands";

        public async ValueTask<LabCommand> PostLabCommand(LabCommand labCommand)
        {
            return await this.apiFactoryClient.PostContentAsync<LabCommand>(
                relativeUrl: $"{LabCommandsApiRelativeUrl}",
                content: labCommand);
        }

        public async ValueTask<LabCommand> GetLabCommandById(Guid id)
        {
            return await this.apiFactoryClient.GetContentAsync<LabCommand>(
                relativeUrl: $"{LabCommandsApiRelativeUrl}/{id}");
        }

        public async ValueTask<LabCommand> PutLabCommand(LabCommand labCommand)
        {
            return await this.apiFactoryClient.PutContentAsync<LabCommand>(
                relativeUrl: $"{LabCommandsApiRelativeUrl}",
                content: labCommand);
        }
    }
}
