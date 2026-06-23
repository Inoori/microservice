using Articles.Abstractions.Enums;
using Submission.Domain.Entities;

namespace Submission.Persistence.Cache;

/// <summary>
/// 表示一个接口，用于缓存资产类型定义的相关信息。
/// </summary>
public interface IAssetTypeDefinitionCache
{
    public ValueTask<AssetTypeDefinition?> GetAsync(
        AssetType id,
        CancellationToken cancellationToken);
}