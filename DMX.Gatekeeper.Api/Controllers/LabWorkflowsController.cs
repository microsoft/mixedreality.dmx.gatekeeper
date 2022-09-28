// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Gatekeeper.Api.Models.LabWorkflows;
using DMX.Gatekeeper.Api.Models.LabWorkflows.Exceptions;
using DMX.Gatekeeper.Api.Services.Foundations.LabWorkflows;
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
    public class LabWorkflowsController : RESTFulController
    {
        private readonly ILabWorkflowService labWorkflowService;

        public LabWorkflowsController(ILabWorkflowService labWorkflowService) =>
            this.labWorkflowService = labWorkflowService;

        [HttpPost]
#if RELEASE
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:PostLabWorkflow")]
#endif
        public async ValueTask<ActionResult<LabWorkflow>> PostLabWorkflowAsync(LabWorkflow labWorkflow)
        {
            try
            {
                LabWorkflow addedLabWorkflow =
                    await this.labWorkflowService.AddLabWorkflowAsync(labWorkflow);

                return Created(addedLabWorkflow);
            }
            catch (LabWorkflowValidationException labWorkflowValidationException)
            {
                return BadRequest(labWorkflowValidationException.InnerException);
            }
            catch (LabWorkflowDependencyException labWorkflowDependencyException)
            {
                return InternalServerError(labWorkflowDependencyException);
            }
            catch (LabWorkflowDependencyValidationException labWorkflowDependencyValidationException)
                when (labWorkflowDependencyValidationException.InnerException is AlreadyExistsLabWorkflowException)
            {
                return Conflict(labWorkflowDependencyValidationException.InnerException);
            }
            catch (LabWorkflowDependencyValidationException labWorkflowDependencyValidationException)
            {
                return BadRequest(labWorkflowDependencyValidationException.InnerException);
            }
            catch (LabWorkflowServiceException labWorkflowServiceException)
            {
                return InternalServerError(labWorkflowServiceException);
            }
        }
    }
}
