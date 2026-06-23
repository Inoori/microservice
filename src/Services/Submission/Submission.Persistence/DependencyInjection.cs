using FileStorage.MongoGridFS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Persistence.Cache;

namespace Submission.Persistence;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPersistenceServices(IConfiguration configuration, bool isDevelopment)
        {
            // asset type definition cache
            services.AddScoped<IAssetTypeDefinitionCache, MemoryAssetTypeDefinitionCache>();

            services.AddMongoFileStorage(configuration);

            return services.AddDbContext<SubmissionDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("Database"));

                //默认查询行为 不跟踪实体更改
                option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                // 开发环境开启 敏感日志记录
                if (isDevelopment) option.EnableSensitiveDataLogging();
            });
        }
    }
}