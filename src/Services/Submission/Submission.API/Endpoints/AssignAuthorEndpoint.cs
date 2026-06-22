using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Articles.Security;
using MediatR;
using Submission.Application.Features.AssignAuthor;

namespace Submission.API.Endpoints;

public static class AssignAuthorEndpoint
{
    /// <summary>
    /// 映射用于分配作者给文章的终点.
    /// </summary>
    /// <param name="app">The endpoint route builder used to define the API routes.</param>
    /// <returns>The updated endpoint route builder with the assign author endpoint mapped.</returns>
    public static IEndpointRouteBuilder MapAssignAuthorEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("api/articles/{articleId:int}/authors/{authorId:int}",
                async (int articleId, int authorId, AssignAuthorCommand command, ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var response = await sender.Send(command with { ArticleId = articleId, AuthorId = authorId },
                        cancellationToken);
                    return TypedResults.Ok(response);
                })
            .RequireRoleAuthorization(Role.CorAut)
            .WithName("AssignAuthor")
            .WithTags(EndpointRegisteration.Articles)
            .Produces<IdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        return app;
    }
}