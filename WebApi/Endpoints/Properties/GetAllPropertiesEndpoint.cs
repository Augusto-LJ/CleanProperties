using Application.Features.Properties.Queries;
using Application.Models.Responses;
using Application.Wrappers;
using MediatR;

namespace WebApi.Endpoints.Properties;

public static class GetAllPropertiesEndpoint
{
    public static RouteHandlerBuilder MapGetAllPropertiesEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint.MapGet("/all", async (ISender sender) =>
        {
            var response = await sender.Send(new GetAllPropertiesQuery());

            if (response.IsSuccessful)
                return Results.Ok(response);

            return Results.NotFound(response);
        })
        .Produces<ResponseWrapper<List<PropertyResponse>>>(StatusCodes.Status200OK)
        .Produces<ResponseWrapper<List<PropertyResponse>>>(StatusCodes.Status404NotFound);
    }
}
