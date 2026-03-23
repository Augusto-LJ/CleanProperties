using Application.Features.Properties.Commands;
using Application.Models.Requests;
using Application.Models.Responses;
using Application.Wrappers;
using MediatR;

namespace WebApi.Endpoints.Properties;

public static class UpdatePropertyEndpoint
{
    public static RouteHandlerBuilder MapUpdatePropertyEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint.MapPut("/update", async (UpdatePropertyRequest request, ISender sender) =>
        {
            var response = await sender.Send(new UpdatePropertyCommand { UpdateProperty = request });

            if (response.IsSuccessful)
                return Results.Ok(response);

            return Results.NotFound(response);
        })
        .Produces<ResponseWrapper<PropertyResponse>>(StatusCodes.Status200OK)
        .Produces<ResponseWrapper<PropertyResponse>>(StatusCodes.Status404NotFound);
    }
}