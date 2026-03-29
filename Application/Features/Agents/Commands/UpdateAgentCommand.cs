using Application.Models.Requests;
using Application.Models.Responses;
using Application.Wrappers;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Agents.Commands;
public class UpdateAgentCommand(UpdateAgentRequest updateAgent) : IRequest<IResponseWrapper>
{
    public UpdateAgentRequest UpdateAgent { get; set; } = updateAgent;
}

public class UpdateAgentCommandHandler(IAgentService agentService) : IRequestHandler<UpdateAgentCommand, IResponseWrapper>
{
    private readonly IAgentService _agentService = agentService;

    public async Task<IResponseWrapper> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
    {
        var agent = request.UpdateAgent.Adapt<Agent>();

        var updatedAgent = await _agentService.UpdateAsync(agent);

        if (updatedAgent is not null)
            return ResponseWrapper<AgentResponse>.Success(data: updatedAgent.Adapt<AgentResponse>(), message: "Agent updated successfully.");

        return ResponseWrapper<AgentResponse>.Fail(message: "Agent does not exist");
    }
}
