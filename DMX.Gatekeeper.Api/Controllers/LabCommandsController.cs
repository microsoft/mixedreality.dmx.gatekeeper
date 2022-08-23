// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabCommands;
using DMX.Gatekeeper.Api.Models.LabCommands.Exceptions;
using DMX.Gatekeeper.Api.Models.Labs.Exceptions;
using DMX.Gatekeeper.Api.Services.Foundations.LabCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

#if RELEASE
using Microsoft.Identity.Web.Resource;
#endif

namespace DMX.Gatekeeper.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LabCommandsController : RESTFulController
    {
        private readonly ILabCommandService labCommandService;

        public LabCommandsController(ILabCommandService labCommandService) =>
            this.labCommandService = labCommandService;

        [HttpPost]
#if RELEASE
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:PostLabCommand")]
#endif
        public async ValueTask<ActionResult<LabCommand>> PostLabCommandAsync(LabCommand labCommand)
        {
            try
            {
                LabCommand addLabCommand =
                    await this.labCommandService.AddLabCommandAsync(labCommand);

                return Created(addLabCommand);
            }
            catch (LabValidationException labValidationException)
            {
                return BadRequest(labValidationException.InnerException);
            }
            catch (LabDependencyException labDependencyException)
            {
                return InternalServerError(labDependencyException);
            }
            catch (LabDependencyValidationException labDependencyValidationException)
            {
                return BadRequest(labDependencyValidationException.InnerException);
            }
            catch (LabServiceException labServiceException)
            {
                return InternalServerError(labServiceException);
            }
        }

        [HttpPut]
#if RELEASE
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:PutLabCommand")]
#endif
        public async ValueTask<ActionResult<LabCommand>> PutLabCommandAsync(LabCommand labCommand)
        {
            try
            {
                LabCommand modifyLabCommand =
                    await this.labCommandService.ModifyLabCommandAsync(labCommand);

                return Ok(modifyLabCommand);
            }
            catch (LabCommandValidationException labCommandValidationException)
                when (labCommandValidationException.InnerException is NotFoundLabCommandException)
            {
                return NotFound(labCommandValidationException.InnerException);
            }
            catch (LabCommandValidationException labCommandValidationException)
            {
                return BadRequest(labCommandValidationException.InnerException);
            }
            catch (LabCommandDependencyValidationException labCommandDependencyValidationException)
                when (labCommandDependencyValidationException.InnerException is AlreadyExistsLabCommandException)
            {
                return Conflict(labCommandDependencyValidationException.InnerException);
            }
            catch (LabCommandDependencyException labCommandDependencyException)
            {
                return InternalServerError(labCommandDependencyException);
            }
            catch (LabCommandServiceException labCommandServiceException)
            {
                return InternalServerError(labCommandServiceException);
            }
        }
    }
}
