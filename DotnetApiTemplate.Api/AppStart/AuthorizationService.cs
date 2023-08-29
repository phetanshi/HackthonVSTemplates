using DotnetApiTemplate.Api.Authorization;
using DotnetApiTemplate.Api.Authorization.Admin;
using DotnetApiTemplate.Api.Authorization.Default;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;

namespace DotnetApiTemplate.Api.AppStart
{
    public static class AuthorizationService
    {
        public static IServiceCollection AddAppAuthorization(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AppPolicies.DEFAULT, new DefaultAuthorizationRequirement());
                options.AddPolicy(AppPolicies.ADMIN, new AdminAuthorizationRequirement());
            });
            return services;
        }

        private static void AddPolicy<T>(this AuthorizationOptions options, string policyName, T requirement) where T : IAuthorizationRequirement
        {
            options.AddPolicy(policyName, policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.AuthenticationSchemes.Add(NegotiateDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(requirement);
            });
        }
    }
}
