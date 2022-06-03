// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Tests.Acceptance.Models.Labs;
using DMX.Gatekeeper.Api.Tests.Acceptance.Brokers;
using Tynamix.ObjectFiller;
using WireMock.Server;
using Xunit;

namespace DMX.Gatekeeper.Api.Tests.Acceptance.APIs.Labs
{
    [Collection(nameof(ApiTestCollection))]
    public partial class LabApiTests
    {
        private readonly DmxGatekeeperApiBroker dmxGatekeeperApiBroker;
        private readonly WireMockServer wireMockServer;

        public LabApiTests(DmxGatekeeperApiBroker dmxGatekeeperApiBroker)
        {
            this.dmxGatekeeperApiBroker = dmxGatekeeperApiBroker;
            this.wireMockServer = WireMockServer.Start(6122);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static bool GetRandomBoolean() => new Random().Next(2) == 1;

        private static int GetRandomPowerLevel() =>
            new IntRange(min: 0, max: 101).GetValue();

        private static List<Lab> CreateRandomLabsData()
        {
            int randomCount = GetRandomNumber();

            List<LabDevice> randomDevices = GetRandomLabDevices();

            var allCases = new List<Lab>
            {
                new Lab
                {
                    Id = Guid.NewGuid(),
                    ExternalId = GetRandomNumber().ToString(),
                    Name = GetRandomString(),
                    Description = GetRandomString(),
                    Status = LabStatus.Available,
                    Devices = randomDevices
                },

                new Lab
                {
                    Id = Guid.NewGuid(),
                    ExternalId = GetRandomNumber().ToString(),
                    Name = GetRandomString(),
                    Description = GetRandomString(),
                    Status = LabStatus.Reserved,
                    Devices = randomDevices
                },

                new Lab
                {
                    Id = Guid.NewGuid(),
                    ExternalId = GetRandomNumber().ToString(),
                    Name = GetRandomString(),
                    Description = GetRandomString(),
                    Status = LabStatus.Offline,
                    Devices = randomDevices
                }
            };

            return Enumerable.Range(start: 0, count: randomCount)
                .Select(iterator => allCases)
                    .SelectMany(@case => @case)
                        .ToList();
        }

        private static List<LabDevice> GetRandomLabDevices()
        {
            string randomPhoneName = GetRandomString();
            string randomHMDName = GetRandomString();
            bool randomHostConnectionStatus = GetRandomBoolean();
            bool randomPhoneConnectionStatus = GetRandomBoolean();
            bool randomHMDConnectionStatus = GetRandomBoolean();
            int randomPhonePowerLevel = GetRandomPowerLevel();
            int randomHMDPowerLevel = GetRandomPowerLevel();

            List<LabDevice> labDevices = new List<LabDevice>
            {
                new LabDevice
                {
                    Id = Guid.NewGuid(),
                    Name = null,
                    Type = LabDeviceType.PC,
                    Category = LabDeviceCategory.Host,
                    PowerLevel = null,

                    Status = randomHostConnectionStatus
                        ? LabDeviceStatus.Online
                        : LabDeviceStatus.Offline,
                },

                new LabDevice
                {
                    Id = Guid.NewGuid(),
                    Name = randomPhoneName,
                    Type = LabDeviceType.Phone,
                    Category = LabDeviceCategory.Attachment,
                    PowerLevel = randomPhonePowerLevel,

                    Status = randomPhoneConnectionStatus
                        ? LabDeviceStatus.Online
                        : LabDeviceStatus.Offline,
                },

                new LabDevice
                {
                    Id = Guid.NewGuid(),
                    Name = randomHMDName,
                    Type = LabDeviceType.HeadMountedDisplay,
                    Category = LabDeviceCategory.Attachment,
                    PowerLevel = randomHMDPowerLevel,

                    Status = randomHMDConnectionStatus
                        ? LabDeviceStatus.Online
                        : LabDeviceStatus.Offline,
                },
            };

            return labDevices;
        }
    }
}
