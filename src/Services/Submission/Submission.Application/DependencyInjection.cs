using Blocks.MediatR.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Submission.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices()
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddValidatorsFromAssembly(assembly) //register fluent validation
                .AddMediatR(options =>
                {
                    options.RegisterServicesFromAssembly(assembly);

                    // 管道中添加验证行为
                    options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                    options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(SetUserIdBehavior<,>));
                });

            return services;
        }
    }
}