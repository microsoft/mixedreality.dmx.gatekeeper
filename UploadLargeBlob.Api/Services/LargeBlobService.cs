using UploadLargeBlob.Api.Brokers;
using UploadLargeBlob.Api.Models;

// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

namespace UploadLargeBlob.Api.Services
{
    public class LargeBlobService : ILargeBlobService
    {
        private IBlobStorageBroker blobStorageBroker;

        public LargeBlobService(IBlobStorageBroker blobStorageBroker) =>
            this.blobStorageBroker = blobStorageBroker;

        public ValueTask<string?> AddLargeBlob(byte[] largeBlobByteArray)
        {
            var binaryData = new BinaryData(largeBlobByteArray);

            return this.blobStorageBroker.UploadLargeBlobAsync(binaryData);
        }
    }
}
