using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SunProject_Infrastructure.Interface;

namespace SunProject_Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddEntityFrameworkSqlServer().AddDbContext<SunProjectContext>
                (opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                builder => builder.MigrationsAssembly(typeof(SunProjectContext).Assembly.FullName)));

            service.AddScoped<ISunProjectContext, SunProjectContext>();

            return service;
        }
    }
}
