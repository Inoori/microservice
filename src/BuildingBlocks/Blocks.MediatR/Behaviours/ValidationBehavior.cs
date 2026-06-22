using FluentValidation;
using MediatR;

namespace Blocks.MediatR.Behaviours;

/// <summary>
/// Represents a validation behavior for the MediatR pipeline. This behavior performs validation
/// on incoming requests before they reach the corresponding request handler by using a collection
/// of FluentValidation validators.
/// </summary>
/// <typeparam name="TRequest">The type of the request being processed by the pipeline.</typeparam>
/// <typeparam name="TResponse">The type of the response returned by the request handler.</typeparam>
/// <param name="validators">A collection of validators applicable to the request type.</param>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults =
            await Task.WhenAll(validators.Select((v => v.ValidateAsync(context, cancellationToken))));

        var failures = validationResults.Where(r => r.Errors.Count != 0)
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Count > 0) throw new ValidationException(failures);

        return await next(cancellationToken);
    }
}