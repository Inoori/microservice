using Blocks.Core;

namespace Submission.Domain.ValueObjects;

/// <summary>
/// 表示文件扩展名的值对象.
/// </summary>
public class FileExtensions
{
    public IReadOnlyList<string> Extensions { get; init; } = null!;

    public bool IsValidExtension(string extension) =>
        Extensions.IsEmpty() // if no extensions are allowed, any extension is valid
        || Extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
}