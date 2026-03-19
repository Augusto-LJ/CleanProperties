using Application.Models.Requests;
using Application.Wrappers;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Properties.Commands;
public class CreatePropertyCommand : IRequest<IResponseWrapper>
{
    public CreatePropertyRequest CreateProperty { get; set; }
}

public class CreatePropertyCommandHandler(IPropertyService propertyService) : IRequestHandler<CreatePropertyCommand, IResponseWrapper>
{
    private readonly IPropertyService _propertyService = propertyService;
    public async Task<IResponseWrapper> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var newProperty = request.CreateProperty.Adapt<Property>();
        var newPropertyId = await _propertyService.CreateAsync(newProperty);

        return ResponseWrapper<int>.Success(data: newPropertyId, message: "Property created successfully.");
    }
}
