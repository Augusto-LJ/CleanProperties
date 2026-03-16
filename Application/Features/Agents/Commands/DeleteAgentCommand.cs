using Application.Wrappers;
using MediatR;

namespace Application.Features.Agents.Commands;
public class DeleteAgentCommand : IRequest<IResponseWrapper>
{
    public int AgentId { get; set; }
}

public class DeleteAgentCommandHandler(IAgentService _agentService) : IRequestHandler<DeleteAgentCommand, IResponseWrapper>
{
    public async Task<IResponseWrapper> Handle(DeleteAgentCommand request, CancellationToken cancellationToken)
    {
        var agentId = await _agentService.DeleteAsync(request.AgentId);

        return ResponseWrapper<int>.Success(data: agentId, message: "Agent deleted successfully.");
    }
}