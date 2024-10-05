using KeyCloakService.KeyCloak;

using Microsoft.AspNetCore.Authorization;

using Newtonsoft.Json.Linq;

namespace KeyCloakService.Handlers
{
    public class HasRoleHandler : AuthorizationHandler<HasRoleRequirement>
    {
        private const string RESOURCE_CLAIM = "resource_access";
        private const string ROLE_KEYWORD = "roles";

        private readonly string _clientId;

        public HasRoleHandler(string clientId)
        {
            _clientId = !string.IsNullOrEmpty(clientId) ? clientId : throw new ArgumentNullException(nameof(clientId));
        }

        public void ValidateRoleRequirement(AuthorizationHandlerContext context, HasRoleRequirement requirement)
        {
            var isSuccess = false;
            var claims = context.User?.Claims;

            if (claims is not null && claims.Any())
            {
                var resourcesAccess = claims.FirstOrDefault(c => c.Type == RESOURCE_CLAIM);

                if (resourcesAccess is not null)
                {
                    var resourceAccessClaimJson = JObject.Parse(resourcesAccess.Value);

                    var roles = resourceAccessClaimJson?.SelectTokens($"$.{_clientId}.{ROLE_KEYWORD}[*]")?
                                .Select(item => item.ToString())
                                .ToList();

                    if (roles is not null && roles.Exists(item => item == requirement.Role))
                    {
                        context.Succeed(requirement);
                        isSuccess = true;
                    }
                }
            }

            if (!isSuccess) context.Fail();
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasRoleRequirement requirement)
        {
            ValidateRoleRequirement(context, requirement);

            return Task.CompletedTask;
        }
    }
}
