using Application.Models.Responses;
using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Agents.Queries;
public class GetAgentByIdQuery : IRequest<IResponseWrapper<AgentResponse>>
{
    public int AgentId { get; set; }
}

public class GetAgentByIdQueryHandler(IAgentService agentService) : IRequestHandler<GetAgentByIdQuery, IResponseWrapper<AgentResponse>>
{
    private readonly IAgentService _agentService = agentService;

    public async Task<IResponseWrapper<AgentResponse>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        var agentInDb = await _agentService.GetByIdAsync(request.AgentId);

        if (agentInDb is not null)
            return ResponseWrapper<AgentResponse>.Success(data: agentInDb.Adapt<AgentResponse>());

        return ResponseWrapper<AgentResponse>.Fail(message: $"No agent found with ID {request.AgentId}");
    }
}
