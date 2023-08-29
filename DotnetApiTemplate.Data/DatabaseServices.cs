using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ps.EfCoreRepository.SqlServer.DependencyInjection;

namespace DotnetApiTemplate.Data
{
    public static class DatabaseServices
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration, string configKeyForConnStr = "AppDbConnection")
        {
            services.AddEfCoreRepository<AppDbContext>(configuration, configKeyForConnStr);
        }
    }
}
