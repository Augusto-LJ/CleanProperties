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
}
