using Microsoft.AspNetCore.Authorization;

namespace KeyCloakService.KeyCloak
{
    public class KeycloakRoleAuthorizationPolicy : AuthorizationPolicy
    {
        public KeycloakRoleAuthorizationPolicy(string role)
            : base(new IAuthorizationRequirement[] { new HasRoleRequirement(role) }, new string[] { })
        {
        }

        public KeycloakRoleAuthorizationPolicy(IEnumerable<IAuthorizationRequirement> requirements, IEnumerable<string> authenticationSchemes) : base(requirements, authenticationSchemes)
        {
        }
    }
}
