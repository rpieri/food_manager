using FoodManager.SharedKernel.Domain.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.SharedKernel.Domain.DependencyInjection
{
    public static class DomainDependencyInjection
    {
        public static void AddDomainDependencyInjection(this IServiceCollection serviceCollection)
            => serviceCollection.AddTransient<NotificationContext>();
    }
}