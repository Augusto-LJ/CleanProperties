using Domain.Entities;

namespace Application.Features.Properties;
public interface IPropertyService
{
    Task<int> CreateAsync(Property newProperty);
    Task<Property> UpdateAsync(Property updateProperty);
    Task<int> DeleteAsync(int id);
    Task<Property> GetByIdAsync(int id);
    Task<List<Property>> GetAllAsync();
    Task<bool> DoesExistAsync(int id);
}
