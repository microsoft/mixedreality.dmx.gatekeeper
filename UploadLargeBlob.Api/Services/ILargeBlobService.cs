using UploadLargeBlob.Api.Models;
// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

namespace UploadLargeBlob.Api.Services
{
    public interface ILargeBlobService
    {
        ValueTask<string?> AddLargeBlob(Stream fileStream);
    }
}
