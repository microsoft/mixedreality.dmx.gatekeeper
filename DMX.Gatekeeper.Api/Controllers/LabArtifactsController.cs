// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions;
using DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

#if RELEASE
using Microsoft.Identity.Web.Resource;
#endif

namespace DMX.Gatekeeper.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LabArtifactsController : RESTFulController
    {
        private readonly ILabArtifactService labArtifactService;

        public LabArtifactsController(ILabArtifactService labArtifactService) =>
            this.labArtifactService = labArtifactService;

        [HttpPost("{streamName}")]
#if RELEASE
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:PostLabArtifact")]
#endif
        public async ValueTask<ActionResult<string>> PostLabArtifactAsync(string streamName)
        {
            try
            {
                var memoryStream = new MemoryStream();
                await Request.Body.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var labArtifact = new LabArtifact
                {
                    Name = streamName,
                    Content = memoryStream
                };

                await this.labArtifactService.AddLabArtifactAsync(labArtifact);

                return Accepted();
            }
            catch (LabArtifactDependencyException labArtifactDependencyException)
            {
                return InternalServerError(labArtifactDependencyException);
            }
            catch (LabArtifactDependencyValidationException labArtifactDependencyValidationException)
                when (labArtifactDependencyValidationException.InnerException is AlreadyExistsLabArtifactException)
            {
                return Conflict(labArtifactDependencyValidationException.InnerException);
            }
            catch (LabArtifactDependencyValidationException labArtifactDependencyValidationException)
            {
                return BadRequest(labArtifactDependencyValidationException.InnerException);
            }
            catch (LabArtifactValidationException labArtifactValidationException)
            {
                return BadRequest(labArtifactValidationException.InnerException);
            }
            catch (LabArtifactServiceException labArtifactServiceException)
            {
                return InternalServerError(labArtifactServiceException);
            }
        }
    }
}