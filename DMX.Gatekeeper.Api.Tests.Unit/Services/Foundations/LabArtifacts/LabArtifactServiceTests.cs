// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using Tynamix.ObjectFiller;
using Xeptions;

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

        public static Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

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
