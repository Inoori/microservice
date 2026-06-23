using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework.Cache;

public interface IEntityCache<TDbContext, TEntity, in TId>
    where TDbContext : DbContext
    where TEntity : class
    where TId : struct
{
    public Task<List<TEntity>?> GetAllAsync(
        CancellationToken cancellationToken
    );
}