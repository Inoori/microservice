using Articles.Abstractions.Enums;
using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public class AssetTypeDefinition
{
    public AssetType Id { get; init; }

    public AssetType Name { get; init; }

    /// <summary>
    /// 指定允许的最大文件大小（以 MB 为单位）.
    /// </summary>
    public byte MaxFileSizeInMb { get; init; }

    /// <summary>
    /// 指定允许的最大文件大小（以字节为单位）。
    /// 该值根据 MaxFileSizeInMB 属性计算得出。
    /// </summary>
    public int MaxFileSizeInBytes => MaxFileSizeInMb * 1024 * 1024;

    public string DefaultFileExtension { get; set; } = null!;

    public FileExtensions AllowedFileExtensions { get; init; } = null!;

    /// <summary>
    /// 指定允许的最大资产数量。
    /// </summary>
    public int MaxAssetCount { get; init; }

    /// <summary>
    /// 指定是否允许多个资产（资源）
    /// </summary>
    public bool AllowMultipleAssets => MaxAssetCount > 1;
}