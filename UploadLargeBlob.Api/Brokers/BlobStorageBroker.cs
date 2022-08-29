// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure.Storage.Blobs;

namespace UploadLargeBlob.Api.Brokers
{
    public class BlobStorageBroker : IBlobStorageBroker
    {
        private BlobServiceClient blobServiceClient;
        private BlobContainerClient blobContainerClient;
        private BlobClient blobClient;

        public BlobStorageBroker()
        {
            string connectionString = "";
            string containerName = "testContainer";
            string fileName = "testFile";

            this.blobServiceClient = new BlobServiceClient(connectionString);
            this.blobContainerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            this.blobClient = this.blobContainerClient.GetBlobClient(fileName);
        }

        public async ValueTask<string?> UploadLargeBlobAsync(BinaryData largeBlob)
        {
            var response = await blobClient.UploadAsync(largeBlob);
            return response.Value.ToString();
        }
    }
}
