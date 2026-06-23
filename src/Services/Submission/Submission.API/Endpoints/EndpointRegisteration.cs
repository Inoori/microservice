using Submission.API.Endpoints;

namespace Submission.API.Endpoints;

public static class EndpointRegisteration
{
    /// <summary>
    /// 表示与文章管理相关的终点路由。
    /// 该常量用于定义文章终点的路由标签以便标识和分组操作。
    /// </summary>
    public const string Articles = "articles";

    /// <summary>
    /// 表示用于访问与资产相关资源的终点路由。
    /// </summary>
    public const string Assets = "Assets";


    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
    {
        return app.MapCreateArticleEndpoint()
            .MapAssignAuthorEndpoint()
            .MapCreateAndAssignAuthorEndpoints();
    }
}