using Microsoft.AspNetCore.Authorization;

namespace DMX.Gatekeeper.Api.Securities.Policies
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public HasScopeRequirement(string[] accessScope)
        {
            AccessScopes = accessScope;
        }

        public string[] AccessScopes { get; }
    }
}
