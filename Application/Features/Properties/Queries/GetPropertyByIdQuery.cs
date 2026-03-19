using Application.Models.Responses;
using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Properties.Queries;
public class GetPropertyByIdQuery : IRequest<IResponseWrapper<PropertyResponse>>
{
    public int Id { get; set; }
}

public class GetPropertyByIdQueryHandler(IPropertyService propertyService) : IRequestHandler<GetPropertyByIdQuery, IResponseWrapper<PropertyResponse>>
{
    private readonly IPropertyService _propertyService = propertyService;
    public async Task<IResponseWrapper<PropertyResponse>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var property = await _propertyService.GetByIdAsync(request.Id);

        if (property is not null)
            return ResponseWrapper<PropertyResponse>.Success(data: property.Adapt<PropertyResponse>());

        return ResponseWrapper<PropertyResponse>.Fail(message: "Property does not exist.");
    }
}
