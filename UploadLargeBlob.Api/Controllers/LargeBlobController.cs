// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using UploadLargeBlob.Api.Models;
using UploadLargeBlob.Api.Services;

namespace UploadLargeBlob.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LargeBlobController : RESTFulController
    {
        private ILargeBlobService largeBlobService;

        public LargeBlobController(ILargeBlobService largeBlobService)
        {
            this.largeBlobService = largeBlobService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<LargeBlob>> PostLargeBlobAsync(byte[] largeBlobByteArray)
        {
            string? addedLargeBlob = await largeBlobService.AddLargeBlob(largeBlobByteArray);

            return Created(addedLargeBlob);
        }
    }
}
