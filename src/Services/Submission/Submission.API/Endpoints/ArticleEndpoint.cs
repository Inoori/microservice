using MediatR;
using Submission.Application.Features.CreateArticle;

namespace Submission.API.Endpoints;

/// <summary>
/// Endpoints related to article management.
/// </summary>
public static class ArticleEndpoint
{
    extension(IEndpointRouteBuilder app)
    {
        public IEndpointRouteBuilder MapArticleEndpoints()
        {
            // Endpoint to create a new article. Only accessible by users with the "Author" role.
            app.MapPost("api/articles", async (CreateArticleCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return Results.Created($"/api/articles/{response.Id}", response);

            }).RequireAuthorization(policy => policy.RequireRole("Author"))
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .WithName("CreateArticle")
            .WithTags("Articles")
            .WithSummary("Creates a new article.")
            .WithDescription("Allows authors to create a new article by providing the title and content.");


            return app;
        }
    }
}


