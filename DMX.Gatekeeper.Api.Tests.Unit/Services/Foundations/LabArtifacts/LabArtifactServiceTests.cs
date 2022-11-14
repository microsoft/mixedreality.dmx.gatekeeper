// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System.IO;
using System.Text;
using Tynamix.ObjectFiller;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabArtifacts
{
    public partial class LabArtifactServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabArtifactService labArtifactService;
        private readonly CompareLogic compareLogic;

        public LabArtifactServiceTests()
        {
            this.dmxApiBrokerMock = new Mock<IDmxApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.compareLogic = new CompareLogic();

            this.labArtifactService = new LabArtifactService(
                dmxApiBroker: this.dmxApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();


        private static Filler<LabArtifact> CreateLabArtifactFiller()
        {
            var filler = new Filler<LabArtifact>();

            var memoryStream =
                new MemoryStream(Encoding.ASCII.GetBytes(GetRandomString()));

            filler.Setup().OnType<MemoryStream>().Use(memoryStream);

            return filler;
        } 

        private static LabArtifact CreateRandomLabArtifact() =>
            CreateLabArtifactFiller().Create();

        private bool SameLabArtifactAs(
            LabArtifact actualLabArtifact,
            LabArtifact expectedLabArtifact) =>
                this.compareLogic.Compare(expectedLabArtifact, actualLabArtifact).AreEqual;
    }
}
