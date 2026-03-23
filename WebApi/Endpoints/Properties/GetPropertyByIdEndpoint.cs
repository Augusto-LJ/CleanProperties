using Application.Features.Properties.Queries;
using Application.Models.Responses;
using Application.Wrappers;
using MediatR;

namespace WebApi.Endpoints.Properties;

public static class GetPropertyByIdEndpoint
{
    public static RouteHandlerBuilder MapGetPropertyByIdEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint.MapGet("/{id}", async (int id, ISender sender) =>
        {
            var response = await sender.Send(new GetPropertyByIdQuery { Id = id });

            if (response.IsSuccessful)
                return Results.Ok(response);

            return Results.NotFound(response);
        })
        .Produces<ResponseWrapper<PropertyResponse>>(StatusCodes.Status200OK)
        .Produces<ResponseWrapper<PropertyResponse>>(StatusCodes.Status404NotFound);
    }
}
