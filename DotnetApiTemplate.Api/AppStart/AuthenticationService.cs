using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Identity.Web;

namespace DotnetApiTemplate.Api.AppStart
{
    public static class AuthenticationService
    {
        public static IServiceCollection AddAppAuthenticationSchemes(this IServiceCollection services, IConfiguration config)
        {
            services.AddAzureAd(config);
            services.AddWindowsAuthentication(config);
            return services;
        }
        private static void AddAzureAd(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(config.GetSection("AzureAd"));
        }
        private static void AddWindowsAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
        }
    }
}
