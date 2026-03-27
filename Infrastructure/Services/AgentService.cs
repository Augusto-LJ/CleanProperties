using Application.Features.Agents;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;
public class AgentService(ApplicationDbContext context) : IAgentService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> CreateAsync(Agent newAgent)
    {
        await _context.Agents.AddAsync(newAgent);
        await _context.SaveChangesAsync();

        return newAgent.Id;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var agentInDb = await _context.Agents.FirstOrDefaultAsync(agent => agent.Id == id);

        if (agentInDb is not null)
        {
            _context.Agents.Remove(agentInDb);
            await _context.SaveChangesAsync();
            return agentInDb.Id;
        }

        return 0;
    }

    public async Task<bool> DoesExistAsync(int id)
    {
        return await _context.Agents.AnyAsync(agent => agent.Id == id);
    }

    public async Task<List<Agent>> GetAllAsync()
    {
        return await _context
            .Agents
            .Include(agent => agent.PropertyListings)
            .Select(agent => new Agent
            {
                Id = agent.Id,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                PhoneNumber = agent.PhoneNumber,
                Email = agent.Email,
                PropertyListings = agent.PropertyListings.Select(property => new Property
                {
                    Id = property.Id,
                    AgentId = property.AgentId,
                    ShortDescription = property.ShortDescription,
                    LongDescription = property.LongDescription,
                    ListingDate = property.ListingDate,
                    Price = property.Price
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<Agent> GetByIdAsync(int id)
    {
        var agentInDb = await _context
            .Agents
            .Include(agent => agent.PropertyListings)
            .Select(agent => new Agent
            {
                Id = agent.Id,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                PhoneNumber = agent.PhoneNumber,
                Email = agent.Email,
                PropertyListings = agent.PropertyListings.Select(property => new Property
                {
                    Id = property.Id,
                    AgentId = property.AgentId,
                    ShortDescription = property.ShortDescription,
                    LongDescription = property.LongDescription,
                    ListingDate = property.ListingDate,
                    Price = property.Price
                }).ToList()
            })
            .FirstOrDefaultAsync(agent => agent.Id == id);

        return agentInDb is not null ? agentInDb : null;
    }

    public async Task<Agent> UpdateAsync(Agent updateAgent)
    {
        if (await DoesExistAsync(updateAgent.Id))
        {
            _context.Agents.Update(updateAgent);
            await _context.SaveChangesAsync();
            return updateAgent;
        }

        return null;
    }
}
