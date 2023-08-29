using DotnetApiTemplate.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace DotnetApiTemplate.Api.Authorization.Admin
{
    public class AdminAuthorizationHandler : AuthorizationHandler<AdminAuthorizationRequirement>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IEmployeeService _employeeService;

        public AdminAuthorizationHandler(IWebHostEnvironment env, IEmployeeService employeeService)
        {
            _env = env;
            _employeeService = employeeService;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAuthorizationRequirement requirement)
        {
            try
            {
                if (!context?.User?.Identity.IsAuthenticated ?? false)
                {
                    context.Fail();
                    return;
                }

                if (_env.EnvironmentName.ToLower() == "development")
                {
                    await context.WindowsAuthentication(requirement, _employeeService, IsSuperAdmin);
                }
                else
                {
                    await context.AzureAdAuthentication(requirement, _employeeService, IsSuperAdmin);
                }
            }
            catch (Exception ex)
            {
                context.Fail();
                throw;
            }
        }

        private async Task<bool> IsSuperAdmin(int employeeId)
        {
            return await Task.FromResult(false);
        }
    }
}
