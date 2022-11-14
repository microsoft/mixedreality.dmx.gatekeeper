// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts;
using Moq;
using Tynamix.ObjectFiller;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabArtifacts
{
    public partial class LabArtifactServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabArtifactService labArtifactService;

        public LabArtifactServiceTests()
        {
            this.dmxApiBrokerMock = new Mock<IDmxApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.labArtifactService = new LabArtifactService(
                dmxApiBroker: this.dmxApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Filler<LabArtifact> CreateLabArtifactFiller() =>
            new Filler<LabArtifact>();

        private static LabArtifact CreateRandomLabArtifact() =>
            CreateLabArtifactFiller().Create();
    }
}
