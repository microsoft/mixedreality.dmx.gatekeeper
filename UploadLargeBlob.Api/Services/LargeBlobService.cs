// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using RESTFulSense.Exceptions;
using UploadLargeBlob.Api.Brokers;

namespace UploadLargeBlob.Api.Services
{
    public class LargeBlobService : ILargeBlobService
    {
        private IBlobStorageBroker blobStorageBroker;

        public LargeBlobService(IBlobStorageBroker blobStorageBroker) =>
            this.blobStorageBroker = blobStorageBroker;

        public ValueTask<string?> AddLargeBlob(Stream fileStream)
        {
            try
            {
                return this.blobStorageBroker.UploadLargeBlobAsync(fileStream);
            }
            catch (HttpResponseException httpResonseException)
            {
                throw httpResonseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
