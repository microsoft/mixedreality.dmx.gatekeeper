// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.LabArtifacts;
using DMX.Gatekeeper.Api.Models.LabArtifacts.Exceptions;
using DMX.Gatekeeper.Api.Services.Foundations.LabArtifacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using System.Threading.Tasks;

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

        [HttpPost]
#if RELEASE
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:PostLabArtifact")]
#endif
        public async ValueTask<ActionResult<LabArtifact>> PostLabArtifactAsync(LabArtifact labArtifact)
        {
            try
            {
                LabArtifact addedLabArtifact =
                    await this.labArtifactService.AddArtifactAsync(labArtifact);

                return Created(addedLabArtifact);
            }
            catch (LabArtifactDependencyException labArtifactDependencyException)
            {
                return InternalServerError(labArtifactDependencyException);
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