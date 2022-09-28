// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System;
using DMX.Gatekeeper.Api.Brokers.DmxApis;
using DMX.Gatekeeper.Api.Brokers.Loggings;
using DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows;
using Moq;
using Tynamix.ObjectFiller;
using DMX.Gatekeeper.Api.Models.LabWorkflows;
using Xunit;
using Xeptions;
using RESTFulSense.Exceptions;
using System.Linq.Expressions;

namespace DMX.Gatekeeper.Api.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabWorkflowService labWorkflowService;

        public LabWorkflowServiceTests()
        {
            this.dmxApiBrokerMock = new Mock<IDmxApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.labWorkflowService = new LabWorkflowService(
                dmxApiBroker: this.dmxApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<Xeption> CriticalDependencyExceptions()
        {
            return new TheoryData<Xeption>
            {
                new HttpResponseUrlNotFoundException(),
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException(),
            };
        }

        public static TheoryData DependencyException()
        {
            return new TheoryData<Xeption>
            {
                new HttpResponseException(),
                new HttpResponseInternalServerErrorException(),
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

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
