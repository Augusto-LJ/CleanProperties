using Application.Pipelines.Contracts;
using Application.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Pipelines;
public class ValidationPipelineBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>, IValidatable  
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                var errorMessages = failures.Select(f => f.ErrorMessage).ToList();
                return (TResponse)ResponseWrapper.Fail(messages: errorMessages);
                throw new ValidationException(failures);
            }
        }

        return await next(cancellationToken);
    }
}
