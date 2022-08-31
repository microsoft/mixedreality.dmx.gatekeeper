// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Azure.Storage.Blobs;
using UploadLargeBlob.Api.Models.Configurations;

namespace UploadLargeBlob.Api.Brokers
{
    public class BlobStorageBroker : IBlobStorageBroker
    {
        private BlobServiceClient blobServiceClient;
        private BlobContainerClient blobContainerClient;
        private BlobClient blobClient;
        private readonly IConfiguration configuration;
        private string connectionString;

        public BlobStorageBroker(IConfiguration configuration)
        {
            string containerName = "testcontainer";
            string fileName = "mediumfile.txt";

            this.configuration = configuration;
            this.connectionString = GetStorageConnectionString(this.configuration);

            this.blobServiceClient = new BlobServiceClient(connectionString);
            this.blobContainerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
            this.blobClient = this.blobContainerClient.GetBlobClient(fileName);
        }

        public async ValueTask<string?> UploadLargeBlobAsync(Stream fileStream)
        {
            try
            {
                await blobClient.UploadAsync(fileStream, false);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            fileStream.Close();
            return "blah";
        }

        private string GetStorageConnectionString(IConfiguration configuration)
        {
            LocalConfiguration localConfigurations =
                configuration.Get<LocalConfiguration>();

            return localConfigurations.StorageConfiguration.ConnectionString;

        }
    }
}
