namespace Blocks.Core;

public static class StringExtensions
{
    public static string FormatWith(this string format, params object?[] args) => string.Format(format, args);

    public static string FormatWith(this string format, object? arg) => string.Format(format, arg);
}