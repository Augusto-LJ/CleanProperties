using Application.Models.Responses;
using Application.Pipelines.Contracts;
using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Agents.Queries;
public class GetAgentByIdQuery : IRequest<ResponseWrapper<AgentResponse>>, ICacheable
{
    public int AgentId { get; set; }
    public string CacheKey { get; set; }
    public bool BypassCache { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }

    public GetAgentByIdQuery(int id)
    {
        AgentId = id;
        CacheKey = $"{nameof(GetAgentByIdQuery)}:{AgentId}";
        BypassCache = false;
    }
}

public class GetAgentByIdQueryHandler(IAgentService agentService) : IRequestHandler<GetAgentByIdQuery, ResponseWrapper<AgentResponse>>
{
    private readonly IAgentService _agentService = agentService;

    public async Task<ResponseWrapper<AgentResponse>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        var agentInDb = await _agentService.GetByIdAsync(request.AgentId);

        if (agentInDb is not null)
            return (ResponseWrapper<AgentResponse>)ResponseWrapper<AgentResponse>.Success(data: agentInDb.Adapt<AgentResponse>());

        return (ResponseWrapper<AgentResponse>)ResponseWrapper<AgentResponse>.Fail(message: $"No agent found with ID {request.AgentId}");
    }
}
