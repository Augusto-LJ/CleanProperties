using Application.Models.Responses;
using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Agents.Queries;
public class GetAllAgentsQuery : IRequest<IResponseWrapper<List<AgentResponse>>>
{
    List<AgentResponse> Agents { get; set; }
}

public class GetAllAgentsQueryHandler(IAgentService agentService): IRequestHandler<GetAllAgentsQuery, IResponseWrapper<List<AgentResponse>>>
{
    private readonly IAgentService _agentService = agentService;
    public async Task<IResponseWrapper<List<AgentResponse>>> Handle(GetAllAgentsQuery request, CancellationToken cancellationToken)
    {
        var agentsInDb = await _agentService.GetAllAsync();

        if (agentsInDb.Count != 0)
            return ResponseWrapper<List<AgentResponse>>.Success(data: agentsInDb.Adapt<List<AgentResponse>>());

        return ResponseWrapper<List<AgentResponse>>.Fail(message: "No agents found.");
    }
}
