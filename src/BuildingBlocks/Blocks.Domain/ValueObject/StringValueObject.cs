namespace Blocks.Domain.ValueObject;

public abstract class StringValueObject
{
    public string Value { get; protected init; } = null!;

    public override string ToString() => Value;

    public override int GetHashCode() => Value.GetHashCode();

    public bool Equals(StringValueObject? obj) => Value.Equals(obj?.Value);

    /// <summary>
    /// 定义一个隐式类型转换操作符，用于将 <see cref="StringValueObject"/> 类型的实例
    /// 转换为字符串类型。
    /// </summary>
    /// <param name="valueObject">
    /// 要转换的 <see cref="StringValueObject"/> 实例。
    /// </param>
    /// <returns>
    /// 转换后的字符串，等同于 <see cref="StringValueObject.Value"/> 的值。
    /// </returns>
    public static implicit operator string(StringValueObject valueObject) => valueObject.Value;
}