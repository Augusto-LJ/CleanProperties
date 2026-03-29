using Application.Models.Responses;
using Application.Pipelines.Contracts;
using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Properties.Queries;
public class GetPropertiesByAgentIdQuery : IRequest<ResponseWrapper<List<PropertyResponse>>>, ICacheable
{
    public int AgentId { get; set; }
    public string CacheKey { get; set; }
    public bool BypassCache { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }

    public GetPropertiesByAgentIdQuery(int agentId)
    {
        AgentId = agentId;
        CacheKey = $"{nameof(GetPropertiesByAgentIdQuery)}:{AgentId}";
        BypassCache = false;
    }
}

public class GetPropertiesByAgentIdQueryHandler(IPropertyService propertyService) : IRequestHandler<GetPropertiesByAgentIdQuery, ResponseWrapper<List<PropertyResponse>>>
{
    private readonly IPropertyService _propertyService = propertyService;
    public async Task<ResponseWrapper<List<PropertyResponse>>> Handle(GetPropertiesByAgentIdQuery request, CancellationToken cancellationToken)
    {
        var propertiesInDb = await _propertyService.GetByAgentIdAsync(request.AgentId);

        if (propertiesInDb.Count != 0)
            return (ResponseWrapper<List<PropertyResponse>>)ResponseWrapper<List<PropertyResponse>>.Success(data: propertiesInDb.Adapt<List<PropertyResponse>>());

        return (ResponseWrapper<List<PropertyResponse>>)ResponseWrapper<List<PropertyResponse>>.Fail(message: "No properties were found for the specified agent.");
    }
}