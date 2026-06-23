using Blocks.Core;
using FileStorage.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS;

public static class FileStorageRegistration
{
    /// <summary>
    /// 为应用程序添加基于MongoDB GridFS的文件存储服务。
    /// </summary>
    /// <param name="service">
    /// 服务容器，用于注册依赖项。
    /// </param>
    /// <param name="configuration">
    /// 应用程序配置，用于检索MongoGridFsFileStorageOptions的设置。
    /// </param>
    /// <returns>
    /// 更新后的服务容器。
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// 当MongoGridFsFileStorageOptions配置节缺失时抛出此异常。
    /// </exception>
    public static IServiceCollection AddMongoFileStorage(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAndValidateOptions<MongoGridFsFileStorageOptions>(configuration);

        
        var options = configuration.GetSection<MongoGridFsFileStorageOptions>();

        service.AddSingleton<IMongoClient>(sp => new MongoClient(options.ConnectionString));

        service.AddSingleton<IMongoDatabase>(sp =>
            sp.GetRequiredService<IMongoClient>().GetDatabase(options.DatabaseName));

        service.AddSingleton<GridFSBucket>(sp =>
        {
            var db = sp.GetRequiredService<IMongoDatabase>();
            return new GridFSBucket(db, new GridFSBucketOptions()
            {
                BucketName = options.BuketName,
                ChunkSizeBytes = options.ChunkSizeBytes,
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Primary
            });
        });

        service.AddSingleton<IFileService, FileService>();

        return service;
    }
}