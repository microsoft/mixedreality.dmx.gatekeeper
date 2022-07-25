using Microsoft.AspNetCore.Authorization;

namespace DMX.Gatekeeper.Api.Securities.Policies
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public HasScopeRequirement(string[] accessScopes)
        {
            AccessScopes = accessScopes;
        }

        public string[] AccessScopes { get; }
    }
}
