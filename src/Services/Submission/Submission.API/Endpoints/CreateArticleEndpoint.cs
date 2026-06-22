using Articles.Abstractions.Enums;
using MediatR;
using Submission.Application.Features.CreateArticle;

namespace Submission.API.Endpoints;

/// <summary>
/// 创建文章端点
/// </summary>
public static class CreateArticleEndpoint
{
    public static IEndpointRouteBuilder MapCreateArticleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles",
                async (CreateArticleCommand request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var response = await sender.Send(request, cancellationToken);
                    return TypedResults.Created($"/api/articles/{response.Id}", response);
                })
            .RequireAuthorization(policy => policy.RequireRole(Role.Aut))
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .WithName("CreateArticle")
            .WithTags(EndpointRegisteration.Articles)
            .WithSummary("Creates a new article.")
            .WithDescription("Allows authors to create a new article by providing the title and content.");

        return app;
    }
}