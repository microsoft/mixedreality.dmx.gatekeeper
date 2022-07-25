using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

namespace DMX.Gatekeeper.Api.Securities.Policies
{
    public class HasScopeRequirementHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            var scopeClaims = context.User.FindAll(ClaimConstants.Scp)
              .Union(context.User.FindAll(ClaimConstants.Scope))
              .ToList();

            if (!scopeClaims.Any())
            {
                return Task.CompletedTask;
            }

            var hasScope = scopeClaims.SelectMany(
                s => s.Value.Split(' '))
                    .Intersect(requirement.AccessScopes)
                        .Any();

            if (hasScope)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
