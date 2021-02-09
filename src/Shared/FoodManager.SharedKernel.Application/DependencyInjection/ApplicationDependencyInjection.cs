using FoodManager.SharedKernel.Application.Behaviors;
using FoodManager.SharedKernel.Application.Commands;
using FoodManager.SharedKernel.Application.Handlers;
using FoodManager.SharedKernel.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.SharedKernel.Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplicationnDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
        }

        public static void AddApplicationMediatR<TCommand>(this IServiceCollection services) where TCommand : Command<TCommand> => services.AddMediatR(typeof(TCommand));
    }
}