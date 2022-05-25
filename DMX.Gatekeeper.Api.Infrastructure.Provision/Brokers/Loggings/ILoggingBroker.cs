// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

namespace DMX.Gatekeeper.Api.Infrastructure.Provision.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogActivity(string message);
    }
}
