namespace DotnetApiTemplate.Api.AppStart
{
    public static class AppServices
    {
        public static WebApplicationBuilder AddAppServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddApplicationObjects();
            builder.Services.AddAppAuthenticationSchemes(builder.Configuration);
            builder.Services.AddAppAuthorization(builder.Configuration);
            builder.Services.AddSwaggerWithAutherization(builder.Configuration);
            return builder;
        }
    }
}
