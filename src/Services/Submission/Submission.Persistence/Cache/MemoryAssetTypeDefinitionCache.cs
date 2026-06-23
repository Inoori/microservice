using Articles.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Submission.Domain.Entities;

namespace Submission.Persistence.Cache;

/// <summary>
/// 表示一个内存中的资产类型定义缓存，提供从数据库或缓存中获取资产类型定义的功能。
/// </summary>
public class MemoryAssetTypeDefinitionCache(SubmissionDbContext dbContext, IMemoryCache cache)
    : IAssetTypeDefinitionCache
{
    /// <summary>
    /// 表示缓存的持续时间。
    /// 该变量定义了缓存的存储时长，具体为 3 小时。
    /// 用于在缓存实现中设置绝对过期时间策略，确保数据在特定时间间隔后进行刷新或失效处理。
    /// </summary>
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(3);

    /// <summary>
    /// 异步获取指定资产类型定义的方法。
    /// </summary>
    /// <param name="id">资产类型的唯一标识符。</param>
    /// <param name="cancellationToken">可选的取消令牌，用于取消操作。</param>
    /// <returns>一个表示资产类型定义的对象。如果未找到对应的资产类型定义，则返回 null。</returns>
    public async ValueTask<AssetTypeDefinition?> GetAsync(
        AssetType id,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = BuildCacheKey((int)id);

        return await cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            return await dbContext.AssetTypeDefinitions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        });
    }

    private static string BuildCacheKey(int id)
    {
        return $"asset-type-definition:{id}";
    }
}