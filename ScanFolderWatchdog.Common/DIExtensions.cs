using Microsoft.Extensions.DependencyInjection;
using ScanFolderWatchdog.Common.Services;

namespace ScanFolderWatchdog.Common
{
    public static class DIExtensions
    {
        public static IServiceCollection UseEnvironmentConfigurationService(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, EnvironmentConfigurationService>();

            return services;
        }

        public static IServiceCollection UseMailNotificationService(this IServiceCollection services)
        {
            services
                .AddSingleton<INotificationService, MailNotificationService>()
                // TODO: Replace with factory
                .AddSingleton<INotificationSettings, MailNotificationSettings>();

            return services;
        }
    }
}
