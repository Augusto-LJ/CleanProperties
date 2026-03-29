using Application.Models.Requests;
using Application.Wrappers;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Agents.Commands;
public class CreateAgentCommand(CreateAgentRequest createAgent) : IRequest<IResponseWrapper>
{
    public CreateAgentRequest CreateAgent { get; set; } = createAgent;
}

public class CreateAgentCommandHandler(IAgentService agentService) : IRequestHandler<CreateAgentCommand, IResponseWrapper>
{
    private readonly IAgentService _agentService = agentService;
    public async Task<IResponseWrapper> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
    {
        var newAgent = request.CreateAgent.Adapt<Agent>();

        var agentId = await _agentService.CreateAsync(newAgent);

        return ResponseWrapper<int>.Success(data: agentId, message: "Agent created successfully.");
    }
}
