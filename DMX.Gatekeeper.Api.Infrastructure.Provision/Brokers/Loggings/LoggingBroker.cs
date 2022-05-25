// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;

namespace DMX.Gatekeeper.Api.Infrastructure.Provision.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        public void LogActivity(string message) =>
            Console.WriteLine(message);
    }
}
