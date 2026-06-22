using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

namespace Submission.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            // Add support for JSON serialization of enums.
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services.AddMemoryCache().AddEndpointsApiExplorer();
    }
}