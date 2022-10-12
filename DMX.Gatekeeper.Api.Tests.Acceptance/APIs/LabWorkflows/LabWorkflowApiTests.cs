// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Tests.Acceptance.Brokers;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.LabWorkflows;
using System;
using Tynamix.ObjectFiller;
using WireMock.Server;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.LabWorkflows
{
    [Collection(nameof(ApiTestCollection))]
    public partial class LabWorkflowApiTests
    {
        private readonly DmxGatekeeperApiBroker dmxGatekeeperApiBroker;
        private readonly WireMockServer wireMockServer;

        public LabWorkflowApiTests(DmxGatekeeperApiBroker dmxGatekeeperApiBroker)
        {
            this.dmxGatekeeperApiBroker = dmxGatekeeperApiBroker;
            this.wireMockServer = WireMockServer.Start(6122);
        }

        private static LabWorkflow CreateRandomLabWorkflow() =>
            CreateLabWorkflowFiller().Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<LabWorkflow> CreateLabWorkflowFiller()
        {
            var filler = new Filler<LabWorkflow>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset);

            return filler;
        }
    }
}
