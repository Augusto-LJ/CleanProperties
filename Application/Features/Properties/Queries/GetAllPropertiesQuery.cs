using Application.Models.Responses;
using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Properties.Queries;
public class GetAllPropertiesQuery : IRequest<IResponseWrapper<List<PropertyResponse>>>
{
}

public class GetAllPropertiesQueryHandler(IPropertyService propertyService) : IRequestHandler<GetAllPropertiesQuery, IResponseWrapper<List<PropertyResponse>>>
{
    private readonly IPropertyService _propertyService = propertyService;
    public async Task<IResponseWrapper<List<PropertyResponse>>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
    {
        var propertiesInDb = await _propertyService.GetAllAsync();

        if (propertiesInDb.Count != 0)
            return ResponseWrapper<List<PropertyResponse>>.Success(data: propertiesInDb.Adapt<List<PropertyResponse>>());

        return ResponseWrapper<List<PropertyResponse>>.Fail(message: "No properties were found.");
    }
}
