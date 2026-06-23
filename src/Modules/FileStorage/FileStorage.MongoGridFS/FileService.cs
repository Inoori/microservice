using FileStorage.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.MongoGridFS;

public class FileService(GridFSBucket bucket, IOptions<MongoGridFsFileStorageOptions> options)
    : IFileService
{
    private const string FilePathMetadataKey = "FilePath";

    private const string ContentTypeMetadataKey = "ContentType";

    private const string DefaultContentType = "application/octet-stream";

    public async Task<UploadResponse> UploadFileAsync(string filePath, IFormFile file, bool overwrite = false,
        Dictionary<string, string>? tags = null)
    {
        if (file.Length > options.Value.FileSizeLimitBytes)
            throw new InvalidOperationException(
                $"File exceeds maximum allowed size of {options.Value.FileSizeLimit} MB");

        var metadata = new BsonDocument(tags ?? new Dictionary<string, string>())
        {
            { FilePathMetadataKey, filePath },
            { ContentTypeMetadataKey, file.ContentType }
        };

        await using var stream = file.OpenReadStream();

        var fileId = await bucket.UploadFromStreamAsync(file.FileName, stream, new GridFSUploadOptions()
        {
            Metadata = metadata,
            ChunkSizeBytes = options.Value.ChunkSizeBytes
        });

        return new UploadResponse(filePath, file.FileName, file.Length, fileId.ToString());
    }

    /// <summary>
    /// 下载指定的文件。
    /// </summary>
    /// <param name="fileId">文件的唯一标识符。</param>
    /// <returns>包含文件流和内容类型的响应对象。</returns>
    /// <exception cref="FileNotFoundException">在文件不存在或文件标识无效时抛出。</exception>
    public async Task<DownloadResponse> DownloadFileAsync(string fileId)
    {
        if (!ObjectId.TryParse(fileId, out var id)) throw new FileNotFoundException($"Invalid file id:{fileId}");

        var fileInfo =
            await (await bucket.FindAsync(Builders<GridFSFileInfo>.Filter.Eq("_id", id))).FirstOrDefaultAsync();

        if (fileInfo is null) throw new FileNotFoundException($"File not found:{fileId}");

        var stream = await bucket.OpenDownloadStreamAsync(id);

        return new DownloadResponse(stream,
            fileInfo.Metadata.GetValue(ContentTypeMetadataKey)?.AsString ?? DefaultContentType);
    }

    /// <summary>
    /// 尝试删除指定的文件。
    /// </summary>
    /// <param name="fileId">文件的唯一标识符。</param>
    /// <returns>如果文件已成功删除，则返回 true；如果文件不存在或删除失败，则返回 false。</returns>
    public async Task<bool> TryDeleteFileAsync(string fileId)
    {
        if (!ObjectId.TryParse(fileId, out var id)) return false;

        try
        {
            await bucket.DeleteAsync(id);
            return true;
        }
        catch (GridFSFileNotFoundException)
        {
            return false;
        }
    }
}