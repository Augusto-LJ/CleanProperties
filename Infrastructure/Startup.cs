using Application.Features.Agents;
using Application.Features.Properties;
using Infrastructure.Contexts;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class Startup
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {    
        return services
            .AddDbContext<ApplicationDbContext>(options => options
                .UseSqlServer(config.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.MigrationsHistoryTable("Migrations", "EFCore");
                }))
            .AddScoped<IAgentService, AgentService>()
            .AddScoped<IPropertyService, PropertyService>();
    }
}
