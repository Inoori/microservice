using Submission.API.Endpoints;

namespace Submission.API.Endpoints;

public static class EndpointRegisteration
{
    /// <summary>
    /// Represents the endpoint route for accessing articles-related resources.
    /// </summary>
    public const string Articles = "articles";


    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
    {
        return app.MapCreateArticleEndpoint()
            .MapAssignAuthorEndpoint()
            .MapCreateAndAssignAuthorEndpoints();
    }
}