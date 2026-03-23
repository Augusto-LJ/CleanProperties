using Application.Features.Properties.Commands;
using Application.Models.Requests;
using Application.Wrappers;
using MediatR;

namespace WebApi.Endpoints.Properties;

public static class CreatePropertyEndpoint
{
    public static RouteHandlerBuilder MapCreatePropertyEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint.MapPost("/add", async (CreatePropertyRequest request, ISender sender) =>
        {
            var response = await sender.Send(new CreatePropertyCommand { CreateProperty = request });

            if (response.IsSuccessful)
                return Results.Ok(response);

            return Results.BadRequest(response);
        })
        .Produces<ResponseWrapper<int>>(StatusCodes.Status200OK)
        .Produces<ResponseWrapper<int>>(StatusCodes.Status400BadRequest);
    }
}
