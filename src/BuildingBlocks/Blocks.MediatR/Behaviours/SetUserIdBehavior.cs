using Blocks.Domain;
using MediatR;

namespace Blocks.MediatR.Behaviours;

public class SetUserIdBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IAuditableAction
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        //todo: get user id from claims
        request.CreatedById = 1;
        return next(cancellationToken);
    }
}