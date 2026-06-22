using Articles.Abstractions;
using Articles.Abstractions.Enums;
using MediatR;
using Submission.Application.Features.CreateAndAssignAuthor;

namespace Submission.API.Endpoints;

public static class CreateAndAssignAuthorEndpoint
{
    public static IEndpointRouteBuilder MapCreateAndAssignAuthorEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles/{articleId:int}/authors",
                async (int articleId, CreateAndAssignAuthorCommand command, ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var response = await sender.Send(command with { ArticleId = articleId }, cancellationToken);
                    return TypedResults.Ok(response);
                })
            .RequireAuthorization(Role.CorAut)
            .WithName("CreateAndAssignAuthor")
            .WithTags(EndpointRegisteration.Articles)
            .Produces<IdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        return app;
    }
}