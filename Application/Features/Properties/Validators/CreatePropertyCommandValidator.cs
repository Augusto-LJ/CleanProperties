using Application.Features.Agents;
using Application.Features.Properties.Commands;
using FluentValidation;

namespace Application.Features.Properties.Validators;
public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator(IAgentService agentService)
    {
        RuleFor(command => command.CreateProperty)
            .SetValidator(new CreatePropertyRequestValidator(agentService));
    }
}
