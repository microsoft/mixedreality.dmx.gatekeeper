// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace DMX.Gatekeeper.Api.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string ArtifactsRelativeUrl = "api/artifacts";

        public async ValueTask PostArtifactAsync(Stream stream) =>
            await PostAsync(ArtifactsRelativeUrl, stream);
    }
}
