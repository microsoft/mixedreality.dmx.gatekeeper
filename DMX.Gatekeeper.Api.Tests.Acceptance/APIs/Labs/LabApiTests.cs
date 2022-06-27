// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using DMX.Gatekeeper.Api.Tests.Acceptance.Brokers;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.Labs;
using Tynamix.ObjectFiller;
using WireMock.Server;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.Labs
{
    [Collection(nameof(ApiTestCollection))]
    public partial class LabApiTests
    {
        private readonly DmxGatekeeperApiBroker dmxGatekeeperApiBroker;
        private readonly WireMockServer wireMockServer;

        public LabApiTests(DmxGatekeeperApiBroker dmxGatekeeperApiBroker)
        {
            this.dmxGatekeeperApiBroker = dmxGatekeeperApiBroker;
            this.wireMockServer = WireMockServer.Start(6122);
        }

        private static List<Lab> CreateRandomLabs() =>
            CreateLabFiller().Create(count: GetRandomNumber()).ToList();

        private static Lab CreateRandomLab() =>
            CreateLabFiller().Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Filler<Lab> CreateLabFiller() =>
            new Filler<Lab>();
    }
}
