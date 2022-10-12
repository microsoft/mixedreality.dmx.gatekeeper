// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Brokers;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.LabCommands;
using Tynamix.ObjectFiller;
using WireMock.Server;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.LabCommands
{
    [Collection(nameof(ApiTestCollection))]
    public partial class LabCommandApiTests
    {
        private readonly DmxGatekeeperApiBroker dmxGatekeeperApiBroker;
        private readonly WireMockServer wireMockServer;

        public LabCommandApiTests(DmxGatekeeperApiBroker dmxGatekeeperApiBroker)
        {
            this.dmxGatekeeperApiBroker = dmxGatekeeperApiBroker;
            this.wireMockServer = WireMockServer.Start(6122);
        }

        private static LabCommand CreateRandomLabCommand() =>
            CreateLabCommandFiller().Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<LabCommand> CreateLabCommandFiller()
        {
            var filler = new Filler<LabCommand>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset);

            return filler;
        }
    }
}
