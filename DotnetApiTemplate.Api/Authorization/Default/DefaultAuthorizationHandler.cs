using DotnetApiTemplate.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace DotnetApiTemplate.Api.Authorization.Default
{
    public class DefaultAuthorizationHandler : AuthorizationHandler<DefaultAuthorizationRequirement>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IEmployeeService _employeeService;

        public DefaultAuthorizationHandler(IWebHostEnvironment env, IEmployeeService employeeService)
        {
            _env = env;
            _employeeService = employeeService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DefaultAuthorizationRequirement requirement)
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
                    await context.WindowsAuthentication(requirement, _employeeService, IsActiveEmployee);
                }
                else
                {
                    await context.AzureAdAuthentication(requirement, _employeeService, IsActiveEmployee);
                }
            }
            catch (Exception ex)
            {
                context.Fail();
                throw;
            }
        }

        private async Task<bool> IsActiveEmployee(int employeeId)
        {
            return await Task.FromResult(true);
        }
    }
}
