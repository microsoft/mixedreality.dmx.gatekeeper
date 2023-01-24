// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts;
using KellermanSoftware.CompareNetObjects;
using Moq;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

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

        public static TheoryData CriticalDependencyException()
        {
            return new TheoryData<Xeption>()
            {
                new HttpResponseUrlNotFoundException(),
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }

        public static TheoryData DependencyException()
        {
            return new TheoryData<Exception>
            {
                new HttpResponseException(),
                new HttpResponseInternalServerErrorException(),
            };
        }

        public static Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Dictionary<string, List<string>> CreateRandomDictionary() =>
            CreateDictionaryFiller().Create();

        private static LabArtifact CreateRandomLabArtifact() =>
            CreateLabArtifactFiller().Create();

        private Expression<Func<LabArtifact, bool>> SameLabArtifactAs(LabArtifact expectedLabArtifact)
        {
            return actualLabArtifact =>
                this.compareLogic.Compare(
                    expectedLabArtifact,
                    actualLabArtifact).AreEqual;
        }

        private static Filler<Dictionary<string, List<string>>> CreateDictionaryFiller() =>
            new Filler<Dictionary<string, List<string>>>();

        private static Filler<LabArtifact> CreateLabArtifactFiller()
        {
            var filler = new Filler<LabArtifact>();

            Stream stream = new MemoryStream();

            filler
                .Setup()
                .OnType<Stream>()
                .Use(stream);

            return filler;
        }
    }
}
