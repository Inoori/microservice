using System.ComponentModel.DataAnnotations;

namespace FileStorage.MongoGridFS;

public class MongoGridFsFileStorageOptions
{
    [Required] public string ConnectionString { get; init; } = null!;

    [Required] public string DatabaseName { get; init; } = null!;

    public string BuketName { get; init; } = "files";

    /// <summary>
    /// 表示 GridFS 中单个文件块的大小（以字节为单位）。
    /// </summary>
    /// <remarks>
    /// 此属性用于设置文件在存储时的分块大小。较大的块大小可能在某些情况下提高性能，
    /// 但可能会增加内存消耗。默认为 1 MB（1024 * 1024 字节）。
    /// </remarks>
    public int ChunkSizeBytes { get; init; } = 1024 * 1024; // 1MB

    /// <summary>
    /// 表示文件的大小限制（以兆字节为单位）。
    /// </summary>
    /// <remarks>
    /// 此属性定义可以上传或存储的单个文件的最大允许大小。默认值为 50 MB。
    /// 如果文件大小超过此限制，将会引发错误或拒绝上传。
    /// </remarks>
    public long FileSizeLimit { get; init; } = 50;

    /// <summary>
    /// 表示文件的大小限制（以字节为单位）。
    /// </summary>
    /// <remarks>
    /// 此属性使用 FileSizeLimit 属性的值并将其转换为字节值。用于定义上传文件的最大允许大小。
    /// 默认值基于 FileSizeLimit 属性（例如，FileSizeLimit 默认为 50，则 FileSizeLimitBytes 为 50 * 1024 * 1024 字节）。
    /// </remarks>
    public long FileSizeLimitBytes => 1024 * 1024 * FileSizeLimit; // 50MB
}