using Articles.Abstractions;
using Blocks.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Submission.Persistence;
using Submission.Persistence.Cache;

namespace Submission.Application.Features.UploadFile;

public class UploadManuscriptFileCommandHandler(
    SubmissionDbContext dbContext,
    IAssetTypeDefinitionCache assetTypeDefinitionCache)
    : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
{
    public async Task<IdResponse> Handle(UploadManuscriptFileCommand command, CancellationToken cancellationToken)
    {
        var article = await dbContext.Articles.AsTracking()
            .Include(a => a.Assets)
            .FirstOrDefaultAsync(a => a.Id == command.ArticleId, cancellationToken);

        if (article is null) throw new NotFoundException("article not found");

        var assetTypeDefinition =
            await assetTypeDefinitionCache.GetAsync(command.AssetType, cancellationToken);

        if (assetTypeDefinition is null) throw new NotFoundException("asset type not found");

        // if multiple assets are allowed, create a new asset
        // if not allowed, check if an asset already exists, if not, create a new one
        var asset = (assetTypeDefinition.AllowMultipleAssets
            ? article.Assets.SingleOrDefault(a => a.Type == assetTypeDefinition.Id)
            : null) ?? article.CreateAsset(assetTypeDefinition);


        //todo: upload file 

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResponse(asset.Id);
    }
}