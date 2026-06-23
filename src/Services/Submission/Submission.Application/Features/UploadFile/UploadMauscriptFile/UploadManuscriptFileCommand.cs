using System.ComponentModel.DataAnnotations;
using Articles.Abstractions.Enums;
using Blocks.Core.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.UploadFile;

public record UploadManuscriptFileCommand : ArticleCommand
{
    [Required] public IFormFile File { get; init; } = null!;

    [Required] public AssetType AssetType { get; init; }

    public override ArticleActionType ActionType => ArticleActionType.Upload;
}

public class UploadManuscriptFileCommandValidator : ArticleCommandValidator<UploadManuscriptFileCommand>
{
    public UploadManuscriptFileCommandValidator()
    {
        RuleFor(c => c.File)
            .NotNullWithMessage(nameof(UploadManuscriptFileCommand.File));

        RuleFor(r => r.AssetType)
            .Must(IsAllowedAssetType)
            .WithMessage(c => $"{c.AssetType} not allowed.");

        //todo - validate file size and file extension
    }

    /// <summary>
    /// 允许的资产类型集合。用于验证上传的文件类型是否合法。
    /// 此属性定义了一个只读集合，包含所有被允许的资产类型，
    /// 如手稿类型等，以确保系统仅接受特定类型的文件。
    /// </summary>
    private IReadOnlyCollection<AssetType> AllowedAssetTypes { get; } = [AssetType.Manuscript];

    /// <summary>
    /// 验证指定的资源类型是否为允许的类型。
    /// </summary>
    /// <param name="assetType">需要验证的资源类型。</param>
    /// <returns>如果资源类型是允许的，则返回 true；否则返回 false。</returns>
    private bool IsAllowedAssetType(AssetType assetType) => AllowedAssetTypes.Contains(assetType);
}