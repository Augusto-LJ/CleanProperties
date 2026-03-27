using Application.Features.Properties.Commands;
using Application.Wrappers;
using MediatR;

namespace WebApi.Endpoints.Properties;

public static class DeletePropertyEndpoint
{
    public static RouteHandlerBuilder MapDeletePropertyEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint.MapDelete("/{id}", async (int id, ISender sender) =>
        {
            var response = await sender.Send(new DeletePropertyCommand { Id = id });

            if (response.IsSuccessful)
                return Results.Ok(response);

            return Results.NotFound(response);
        })
        .WithName(nameof(DeletePropertyEndpoint))
        .WithSummary("Deletes a Property")
        .WithDescription("This endpoint is used to delete a Property with the specified id.")
        .Produces<ResponseWrapper<int>>(StatusCodes.Status200OK)
        .Produces<ResponseWrapper<int>>(StatusCodes.Status404NotFound);
    }
}
