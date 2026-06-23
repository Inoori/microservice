namespace Blocks.Core;

public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// 检查枚举集合是否为空。
        /// </summary>
        /// <returns>如果集合为空，则返回 true；否则返回 false。</returns>
        public bool IsEmpty() => !source.Any();
    }
}