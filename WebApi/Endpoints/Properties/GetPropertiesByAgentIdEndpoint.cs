using Application.Features.Properties.Queries;
using Application.Models.Responses;
using Application.Wrappers;
using MediatR;

namespace WebApi.Endpoints.Properties;

public static class GetPropertiesByAgentIdEndpoint
{
    public static RouteHandlerBuilder MapGetPropertiesByAgentIdEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint.MapGet("/agent/{agentId}", async (int agentId, ISender sender) =>
        {
            var response = await sender.Send(new GetPropertiesByAgentIdQuery(agentId));

            if (response.IsSuccessful)
                return Results.Ok(response);

            return Results.NotFound(response);
        })
        .WithName(nameof(GetPropertiesByAgentIdEndpoint))
        .WithSummary("Retrieves all Properties of an agent")
        .WithDescription("This endpoint is used to retrieve all existing Properties of an agent with specified agentId.")
        .Produces<ResponseWrapper<List<PropertyResponse>>>(StatusCodes.Status200OK)
        .Produces<ResponseWrapper<List<PropertyResponse>>>(StatusCodes.Status404NotFound);
    }
}
