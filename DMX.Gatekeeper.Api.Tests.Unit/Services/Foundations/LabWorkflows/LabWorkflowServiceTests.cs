// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System;
using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Models.LabCommands;
using DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows;
using Moq;
using Tynamix.ObjectFiller;
using DMX.Gatekeeper.Api.Models.LabWorkflows;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBroker;
        private readonly Mock<ILoggingBroker> loggingBroker;
        private readonly ILabWorkflowService labWorkflowService;

        public LabWorkflowServiceTests()
        {
            this.dmxApiBroker = new Mock<IDmxApiBroker>();
            this.loggingBroker = new Mock<ILoggingBroker>();

            this.labWorkflowService = new LabWorkflowService(
                this.dmxApiBroker.Object,
                this.loggingBroker.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static LabWorkflow CreateRandomLabWorkflow() =>
            CreateLabWorkflowFiller().Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<LabWorkflow> CreateLabWorkflowFiller()
        {
            var filler = new Filler<LabWorkflow>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset);

            return filler;
        }

        private static Filler<Dictionary<string, List<string>>> CreateDictionaryFiller() =>
            new Filler<Dictionary<string, List<string>>>();

        private static Dictionary<string, List<string>> CreateRandomDictionary() =>
            CreateDictionaryFiller().Create();
    }
}
