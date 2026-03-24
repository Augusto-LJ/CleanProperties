using Application;
using WebApi.Endpoints.Properties;

namespace WebApi;

public static class Startup
{
    public static void MapPropertyEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var propertyGroup = endpoint.MapGroup("api/properties").WithTags("Properties");
        propertyGroup.MapCreatePropertyEndpoint();
        propertyGroup.MapUpdatePropertyEndpoint();
        propertyGroup.MapDeletePropertyEndpoint();
        propertyGroup.MapGetPropertyByIdEndpoint();
        propertyGroup.MapGetAllPropertiesEndpoint();
        propertyGroup.MapGetPropertiesByAgentIdEndpoint();
    }

    public static CacheSettings GetCacheSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheSettingsConfig = configuration.GetSection("CacheSettings");
        services.Configure<CacheSettings>(cacheSettingsConfig);
        return cacheSettingsConfig.Get<CacheSettings>();
    }
}
