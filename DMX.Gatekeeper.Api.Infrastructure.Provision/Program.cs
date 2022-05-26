// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Infrastructure.Provision.Services.Processings.CloudManagements;

namespace DMX.Gatekeeper.Api.Infrastructure.Provision
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var cloudManagementProcessingService =
               new CloudManagementProcessingService();

            await cloudManagementProcessingService.ProcessAsync();
        }
    }
}