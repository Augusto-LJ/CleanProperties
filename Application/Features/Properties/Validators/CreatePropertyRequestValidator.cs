using Application.Features.Agents;
using Application.Models.Requests;
using FluentValidation;

namespace Application.Features.Properties.Validators;
public class CreatePropertyRequestValidator :AbstractValidator<CreatePropertyRequest>
{
    public CreatePropertyRequestValidator(IAgentService agentService)
    {
        RuleFor(request => request.ShortDescription)
            .NotEmpty().WithMessage("Short description is required.");

        RuleFor(request => request.Price)
            .GreaterThan(0.0M).WithMessage("Price must be a positive number.");

        RuleFor(request => request.AgentId)
            .NotEmpty()
                .WithMessage("AgentId is required.")
            .GreaterThan(0)
                .WithMessage("AgentId must be a positive number.")
            .MustAsync(async (agentId, cancellation) =>
            {
                var agentExists = await agentService.DoesExistAsync(agentId);
                return agentExists;
            })
                .WithMessage("Agent with the specified ID does not exist.");
    }
}
