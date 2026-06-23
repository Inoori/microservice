using FluentValidation;

namespace Blocks.Core.FluentValidation;

public static class Extensions
{
    /// <param name="rule">规则构建器选项用于定义验证规则。</param>
    /// <typeparam name="T">被验证对象的类型.</typeparam>
    /// <typeparam name="TProperty">被验证属性的类型</typeparam>
    extension<T, TProperty>(IRuleBuilderOptions<T, TProperty> rule)
    {
        /// <summary>
        /// 配置一个带有预定义错误信息的验证规则，表明指定的属性必须代表有效的ID。
        /// </summary>
        /// <param name="propertyName">被验证属性的名称，将包含在错误信息中。</param>
        /// <returns>规则构建选项带有错误信息。</returns>
        public IRuleBuilderOptions<T, TProperty> WithMessageForInvalidId(string propertyName) =>
            rule.WithMessage(c => ValidationMessages.InvalidId.FormatWith(propertyName));

        /// <summary>
        /// 配置一个带有预定义错误信息的验证规则，要求指定的属性不能为空或空值。
        /// </summary>
        /// <param name="propertyName">被验证属性的名称，将包含在错误信息中。</param>
        /// <returns>规则构建选项带有错误信息。</returns>
        public IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage(string propertyName) =>
            rule.NotEmpty().WithMessage(c => ValidationMessages.NullOrEmptyValue.FormatWith(propertyName));

        /// <summary>
        /// 配置一个带有预定义错误信息的验证规则，要求指定的属性不能为空值。
        /// </summary>
        /// <param name="propertyName">被验证属性的名称，将包含在错误信息中。</param>
        /// <returns>规则构建选项带有错误信息。</returns>
        public IRuleBuilderOptions<T, TProperty> NotNullWithMessage(string propertyName) =>
            rule.NotNull().WithMessage(c => ValidationMessages.NullOrEmptyValue.FormatWith(propertyName));
    }


    extension<T, TProperty>(IRuleBuilderInitial<T, TProperty> rule)
    {
        /// <summary>
        /// 配置一个带有预定义错误信息的验证规则，要求指定的属性不能为空或空值。
        /// </summary>
        /// <param name="propertyName">被验证属性的名称，将包含在错误信息中。</param>
        /// <returns>规则构建选项带有错误信息。</returns>
        public IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage(string propertyName) =>
            rule.NotEmpty().WithMessage(c => ValidationMessages.NullOrEmptyValue.FormatWith(propertyName));

        /// <summary>
        /// 配置一个带有预定义错误信息的验证规则，要求指定的属性不能为空值。
        /// </summary>
        /// <param name="propertyName">被验证属性的名称，将包含在错误信息中。</param>
        /// <returns>规则构建选项带有错误信息。</returns>
        public IRuleBuilderOptions<T, TProperty> NotNullWithMessage(string propertyName) =>
            rule.NotNull().WithMessage(c => ValidationMessages.NullOrEmptyValue.FormatWith(propertyName));
    }

    extension<T>(IRuleBuilderOptions<T, string?> rule)
    {
        public IRuleBuilderOptions<T, string?> MaximumLengthWithMessage(int maxLength, string propertyName) =>
            rule.MaximumLength(maxLength)
                .WithMessage(c => ValidationMessages.MaxLengthExceeded.FormatWith(propertyName, maxLength));
    }

    extension<T>(IRuleBuilderInitial<T, string?> rule)
    {
        public IRuleBuilderOptions<T, string?> MaximumLengthWithMessage(int maxLength, string propertyName) =>
            rule.MaximumLength(maxLength)
                .WithMessage(c => ValidationMessages.MaxLengthExceeded.FormatWith(propertyName, maxLength));
    }
}