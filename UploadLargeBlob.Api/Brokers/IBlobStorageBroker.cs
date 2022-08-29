// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using UploadLargeBlob.Api.Models;

namespace UploadLargeBlob.Api.Brokers
{
    public interface IBlobStorageBroker
    {
        ValueTask<string?> UploadLargeBlobAsync(BinaryData largeBlob);
    }
}
