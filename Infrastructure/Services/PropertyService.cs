using Application.Features.Properties;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;
public class PropertyService(ApplicationDbContext context) : IPropertyService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<int> CreateAsync(Property newProperty)
    {
        newProperty.ListingDate = DateTime.UtcNow;
        await _context.Properties.AddAsync(newProperty);
        await _context.SaveChangesAsync();

        return newProperty.Id;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var propertyInDb = await _context.Properties.FirstOrDefaultAsync(property => property.Id == id);

        if (propertyInDb is not null)
        {
            _context.Properties.Remove(propertyInDb);
            await _context.SaveChangesAsync();
            return propertyInDb.Id;
        }

        return 0;
    }

    public async Task<bool> DoesExistAsync(int id)
    {
        return await _context.Properties.AnyAsync(property => property.Id == id);
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _context
            .Properties
            .Include(property => property.Agent)
            .Select(property => new Property
            {
                Id = property.Id,
                AgentId = property.AgentId,
                ShortDescription = property.ShortDescription,
                LongDescription = property.LongDescription,
                Price = property.Price,
                ListingDate = property.ListingDate,
                Agent = new Agent
                {
                    Id = property.AgentId,
                    FirstName = property.Agent.FirstName,
                    LastName = property.Agent.LastName,
                    Email = property.Agent.Email,
                    PhoneNumber = property.Agent.PhoneNumber
                }
            })
            .ToListAsync();
    }

    public async Task<Property> GetByIdAsync(int id)
    {
        var propertyInDb = await _context
            .Properties
            .Include(property => property.Agent)
            .Select(property => new Property
            {
                Id = property.Id,
                AgentId = property.AgentId,
                ShortDescription = property.ShortDescription,
                LongDescription = property.LongDescription,
                Price = property.Price,
                ListingDate = property.ListingDate,
                Agent = new Agent
                {
                    Id = property.AgentId,
                    FirstName = property.Agent.FirstName,
                    LastName = property.Agent.LastName,
                    Email = property.Agent.Email,
                    PhoneNumber = property.Agent.PhoneNumber
                }
            })
            .FirstOrDefaultAsync(property => property.Id == id);

        return propertyInDb is not null ? propertyInDb : null;
    }

    public async Task<Property> UpdateAsync(Property updateProperty)
    {
        if (await DoesExistAsync(updateProperty.Id))
        {
            _context.Properties.Update(updateProperty);
            await _context.SaveChangesAsync();
            return updateProperty;
        }

        return null;
    }

    public async Task<List<Property>> GetByAgentIdAsync(int agentId)
    {
        return await _context
            .Properties
            .Include(property => property.Agent)
            .Select(property => new Property
            {
                Id = property.Id,
                AgentId = property.AgentId,
                ShortDescription = property.ShortDescription,
                LongDescription = property.LongDescription,
                Price = property.Price,
                ListingDate = property.ListingDate,
                Agent = new Agent
                {
                    Id = property.AgentId,
                    FirstName = property.Agent.FirstName,
                    LastName = property.Agent.LastName,
                    Email = property.Agent.Email,
                    PhoneNumber = property.Agent.PhoneNumber
                }
            })
            .Where(property => property.AgentId == agentId).ToListAsync();
    }
}
