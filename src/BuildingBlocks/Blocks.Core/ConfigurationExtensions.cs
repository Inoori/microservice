using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blocks.Core;

public static class ConfigurationExtensions
{
    /// <summary>
    /// 添加并验证指定类型的选项配置。
    /// </summary>
    /// <typeparam name="TOptions">要绑定和验证的选项类型。</typeparam>
    /// <param name="service">用于注册服务的服务集合。</param>
    /// <param name="configuration">应用程序的配置对象。</param>
    /// <returns>更新后的服务集合。</returns>
    /// <exception cref="InvalidOperationException">当指定的配置节不存在时引发该异常。</exception>
    public static IServiceCollection AddAndValidateOptions<TOptions>(this IServiceCollection service,
        IConfiguration configuration) where TOptions : class
    {
        var section = configuration.GetSection(typeof(TOptions).Name);

        if (!section.Exists()) throw new InvalidOperationException($"Configuration section '{section.Key}' is missing");

        // register options
        service.AddOptions<TOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return service;
    }

    /// <summary>
    /// 获取指定类型的配置节并将其绑定为强类型对象。
    /// </summary>
    /// <typeparam name="TOptions">目标配置节的强类型表示。</typeparam>
    /// <param name="configuration">应用程序的配置对象。</param>
    /// <returns>绑定到指定配置节的强类型对象。</returns>
    /// <exception cref="ArgumentNullException">当指定的配置节不存在时引发该异常。</exception>
    public static TOptions GetSection<TOptions>(this IConfiguration configuration) where TOptions : class
    {
        var section = configuration.GetSection(typeof(TOptions).Name);

        return !section.Exists()
            ? throw new ArgumentNullException($"section {typeof(TOptions).Name} not found")
            : configuration.GetSection(typeof(TOptions).Name).Get<TOptions>()!;
    }
}