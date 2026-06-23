using Blocks.Domain.ValueObject;
using Submission.Domain.Entities;

namespace Submission.Domain.ValueObjects;

/// <summary>
/// 表示文件名的值对象。
/// </summary>
/// <remarks>
/// 此类继承自 <see cref="StringValueObject"/>，用于封装文件名相关的值及其行为。
/// </remarks>
public class FileName : StringValueObject
{
    private FileName(string value) => Value = value;

    public static FileName Create(Asset asset, FileExtension extension)
    {
        var assetName = asset.Name.Value;
        return new FileName($"{assetName}.{extension}");
    }
}