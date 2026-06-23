using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blocks.EntityFramework.Cache;

/// <summary>
/// MemoryEntityCache 是一个泛型类，用于在内存中缓存实体数据。
/// 它允许通过内存缓存实现对数据库访问的优化，从而减少频繁的数据库查询操作。
/// </summary>
/// <typeparam name="TDbContext">数据库上下文类型，必须继承自 DbContext。</typeparam>
/// <typeparam name="TEntity">实体类型，必须是引用类型。</typeparam>
/// <typeparam name="TId">实体的标识符类型，必须为结构体。</typeparam>
/// <param name="dbContext">数据库上下文实例，用于访问实体数据。</param>
/// <param name="cache">内存缓存实例，用于存储和管理实体数据缓存。</param>
public class MemoryEntityCache<TDbContext, TEntity, TId>(TDbContext dbContext, IMemoryCache cache)
    : IEntityCache<TDbContext, TEntity, TId>
    where TDbContext : DbContext
    where TEntity : class
    where TId : struct
{
    public async Task<List<TEntity>?> GetAllAsync(CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync(typeof(TEntity).FullName!, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow =
                TimeSpan.FromHours(1);

            return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        });
    }
}