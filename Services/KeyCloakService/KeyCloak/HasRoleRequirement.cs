using Microsoft.AspNetCore.Authorization;

namespace KeyCloakService.KeyCloak
{
    public class HasRoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public HasRoleRequirement(string role)
        {
            Role = role ?? throw new ArgumentNullException(nameof(role));
        }
    }
}
