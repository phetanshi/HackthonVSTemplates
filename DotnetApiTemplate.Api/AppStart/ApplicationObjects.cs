using DotnetApiTemplate.Api.Auth;
using DotnetApiTemplate.Api.AutoMapperProfiles;
using DotnetApiTemplate.Api.Services;
using DotnetApiTemplate.Api.Services.Definitions;
using Microsoft.AspNetCore.Authorization;

namespace DotnetApiTemplate.Api.AppStart
{
    public static class ApplicationObjects
    {
        public static IServiceCollection AddApplicationObjects(this IServiceCollection services)
        {
            services.AddServiceDependencies();
            services.AddOthes();
            return services;
        }

        private static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAppLogService, AppLogService>();
        }
        private static void AddOthes(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, AppAuthorizationHandler>();
            services.AddAutoMapper(typeof(EmployeeAutoMapperProfile));
        }
    }
}
