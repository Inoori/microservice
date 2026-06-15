namespace Submission.API.Endpoints;

/// <summary>
/// Endpoints related to article management.
/// </summary>
public static class ArticleEndpoint
{
    extension(WebApplication app)
    {
        public WebApplication MapArticleEndpoints()
        {
            // Endpoint to create a new article. Only accessible by users with the "Author" role.
            app.MapPost("api/articles", (CreateArticleRequest request) =>
            {
                throw new NotImplementedException();

            }).RequireAuthorization(policy => policy.RequireRole("Author"))
            .WithName("CreateArticle")
            .WithTags("Articles")
            .WithSummary("Creates a new article.")
            .WithDescription("Allows authors to create a new article by providing the title and content.");


            return app;
        }
    }
}


public record CreateArticleRequest(string Title, string Content);