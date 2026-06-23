using Articles.Abstractions.Enums;
using Blocks.Domain.ValueObject;
using Submission.Domain.Entities;

namespace Submission.Domain.ValueObjects;

/// <summary>
/// 表示文件扩展名的值对象。
/// </summary>
/// <remarks>
/// 此类继承自 <see cref="StringValueObject"/>，用于封装文件扩展名相关的值及其行为。
/// 提供了根据文件名和资产类型创建文件扩展名的功能。
/// </remarks>
public class FileExtension : StringValueObject
{
    private FileExtension(string value) => Value = value;

    public static FileExtension FromFileName(string fileName, AssetTypeDefinition assetTypeDefinition)
    {
        var extension = Path.GetExtension(fileName).Remove(0, 1); //remove the '.'

        ArgumentException.ThrowIfNullOrWhiteSpace(extension);
        // extension must be in the allowed extensions list 
        ArgumentOutOfRangeException.ThrowIfNotEqual(
            assetTypeDefinition.AllowedFileExtensions.IsValidExtension(extension), true);

        return new FileExtension(extension);
    }
}