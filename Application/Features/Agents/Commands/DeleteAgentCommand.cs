using Application.Wrappers;
using MediatR;

namespace Application.Features.Agents.Commands;
public class DeleteAgentCommand(int agentId) : IRequest<IResponseWrapper>
{
    public int AgentId { get; set; } = agentId;
}

public class DeleteAgentCommandHandler(IAgentService _agentService) : IRequestHandler<DeleteAgentCommand, IResponseWrapper>
{
    public async Task<IResponseWrapper> Handle(DeleteAgentCommand request, CancellationToken cancellationToken)
    {
        var agentId = await _agentService.DeleteAsync(request.AgentId);

        if (agentId is not 0)
            return ResponseWrapper<int>.Success(data: agentId, message: "Agent deleted successfully.");

        return ResponseWrapper<int>.Fail(message: "Agent does not exist.");
    }
}