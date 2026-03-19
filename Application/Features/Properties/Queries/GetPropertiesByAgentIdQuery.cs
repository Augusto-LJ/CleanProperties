using Application.Models.Responses;
using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Properties.Queries;
public class GetPropertiesByAgentIdQuery : IRequest<IResponseWrapper<List<PropertyResponse>>>
{
    public int AgentId { get; set; }
}

public class GetPropertiesByAgentIdQueryHandler(IPropertyService propertyService) : IRequestHandler<GetPropertiesByAgentIdQuery, IResponseWrapper<List<PropertyResponse>>>
{
    private readonly IPropertyService _propertyService = propertyService;
    public async Task<IResponseWrapper<List<PropertyResponse>>> Handle(GetPropertiesByAgentIdQuery request, CancellationToken cancellationToken)
    {
        var propertiesInDb = await _propertyService.GetByAgentIdAsync(request.AgentId);

        if (propertiesInDb.Count != 0)
            return ResponseWrapper<List<PropertyResponse>>.Success(data: propertiesInDb.Adapt<List<PropertyResponse>>());

        return ResponseWrapper<List<PropertyResponse>>.Fail(message: "No properties were found for the specified agent.");
    }
}