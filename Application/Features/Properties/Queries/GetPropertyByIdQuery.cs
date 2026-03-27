using Application.Models.Responses;
using Application.Pipelines.Contracts;
using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Properties.Queries;
public class GetPropertyByIdQuery : IRequest<ResponseWrapper<PropertyResponse>>, ICacheable
{
    public int Id { get; set; }
    public string CacheKey { get; set; }
    public bool BypassCache { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }

    public GetPropertyByIdQuery(int id)
    {
        Id = id;
        CacheKey = $"{nameof(GetPropertyByIdQuery)}:{Id}";
        BypassCache = false;
    }
}

public class GetPropertyByIdQueryHandler(IPropertyService propertyService) : IRequestHandler<GetPropertyByIdQuery, ResponseWrapper<PropertyResponse>>
{
    private readonly IPropertyService _propertyService = propertyService;
    public async Task<ResponseWrapper<PropertyResponse>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var property = await _propertyService.GetByIdAsync(request.Id);

        if (property is not null)
            return (ResponseWrapper<PropertyResponse>)ResponseWrapper<PropertyResponse>.Success(data: property.Adapt<PropertyResponse>());

        return (ResponseWrapper<PropertyResponse>)ResponseWrapper<PropertyResponse>.Fail(message: "Property does not exist.");
    }
}
