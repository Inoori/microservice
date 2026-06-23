using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Articles.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.Features.UploadFile;

namespace Submission.API.Endpoints;

public static class UploadManuscriptFileEndpoint
{
    public static IEndpointRouteBuilder MapUploadManuscriptFileEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/articles/{articleId:int}/assets/manuscript:upload",
                async ([FromRoute] int articleId, [FromForm] UploadManuscriptFileCommand command, ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var response = await sender.Send(command with { ArticleId = articleId }, cancellationToken);
                    return TypedResults.Created($"/api/articles/{articleId}/assets/{response.Id}:download", response);
                })
            .RequireRoleAuthorization(Role.CorAut)
            .WithName("UploadManuscriptFile")
            .WithTags(EndpointRegisteration.Assets)
            .Produces<IdResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .DisableAntiforgery(); // because of the file upload


        return app;
    }
}