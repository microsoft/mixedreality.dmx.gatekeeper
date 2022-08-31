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

        [HttpPost("files"), DisableRequestSizeLimit]
        public async ValueTask<ActionResult<LargeBlob>> PostLargeBlobAsync()
        {
            //var fileStream = Request.Form.Files[0].OpenReadStream();
            var fileStream = Request.Body;
            await this.largeBlobService.AddLargeBlob(fileStream);
            return Ok("yay!");
        }
    }
}
