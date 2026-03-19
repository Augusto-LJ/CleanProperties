using Application.Models.Requests;
using Application.Models.Responses;
using Application.Wrappers;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Properties.Commands;
public class UpdatePropertyCommand : IRequest<ResponseWrapper>
{
    public UpdatePropertyRequest UpdateProperty { get; set; }
}

public class UpdatePropertyCommandHandler(IPropertyService propertyService) : IRequestHandler<UpdatePropertyCommand, IResponseWrapper>
{
    private readonly IPropertyService _propertyService = propertyService;

    public async Task<IResponseWrapper> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = request.UpdateProperty.Adapt<Property>();

        var updatedProperty = await _propertyService.UpdateAsync(property);

        if (updatedProperty is not null )
            return ResponseWrapper<PropertyResponse>.Success(data: updatedProperty.Adapt<PropertyResponse>(), message: "Property updated successfully");

        return ResponseWrapper<PropertyResponse>.Fail("Failed to update property");
    }
}
