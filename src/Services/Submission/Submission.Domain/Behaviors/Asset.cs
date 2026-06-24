using Articles.Abstractions.Enums;
using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class Asset
{
    private Asset()
    {
    }

    /// <summary>
    /// 创建一个新的资产实例。
    /// </summary>
    /// <param name="article">与资产关联的文章实例。</param>
    /// <param name="assetTypeDefinition">资产的类型定义。</param>
    /// <returns>创建的资产实例。</returns>
    internal static Asset Create(Article article, AssetTypeDefinition assetTypeDefinition) => new()
    {
        ArticleId = article.Id,
        Article = article,
        Name = AssetName.AssetTypeDefinition(assetTypeDefinition),
        Type = assetTypeDefinition.Name,
    };

    /// <summary>
    /// 生成存储文件的路径。
    /// </summary>
    /// <param name="fileName">文件名称。</param>
    /// <returns>存储文件的完整路径。</returns>
    public string GenerateStorageFilePath(string fileName) => $"Articles/{ArticleId}/{Name}/{fileName}";
}