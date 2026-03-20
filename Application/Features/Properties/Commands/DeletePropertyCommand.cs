using Application.Wrappers;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Properties.Commands;
public class DeletePropertyCommand : IRequest<IResponseWrapper>
{
    public int Id { get; set; }
}

public class DeletePropertyCommandHandler(IPropertyService propertyService) : IRequestHandler<DeletePropertyCommand, IResponseWrapper>
{
    private readonly IPropertyService _propertyService = propertyService;
    public async Task<IResponseWrapper> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        var deletedPropertyId = await _propertyService.DeleteAsync(request.Id);

        if (deletedPropertyId != 0)
            return ResponseWrapper<int>.Success(data: deletedPropertyId, message: "Property deleted successfully.");

        return ResponseWrapper<int>.Fail(message: "Property does not exist.");
    }
}
