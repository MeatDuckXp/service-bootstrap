using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceBootstrap;
using Test.Sample.Service.Core;
using Test.Sample.Service.Services.Message;
using Test.Sample.Service.Services.NotificationSender;

namespace Test.Sample.Service.Infrastructure
{
    public class TestServiceBootstrap : ServiceBootstrapper
    {
        private IConfigurationRoot _configurationRoot;

        public override void RegisterServices(
            HostBuilderContext builderContext,
            IServiceCollection serviceCollection,
            object configuration,
            IList<Assembly> domainAssemblies = null)
        {
            var serviceConfigurationSection = _configurationRoot.GetSection(nameof(ServiceConfiguration));
            serviceCollection.Configure<ServiceConfiguration>(serviceConfigurationSection); 


            serviceCollection.AddSingleton<INotificationMessageSource, NotificationMessageSource>();
            serviceCollection.AddSingleton<INotificationSenderService, NotificationSenderService>();

            serviceCollection.AddHostedService<NotificationWorker>();
        }

        public override void ConfigureLogging(ILoggingBuilder loggingBuilder, IConfiguration configuration)
        {
             
        }

        public override object LoadConfiguration(IHostEnvironment hostingEnvironment, IConfigurationBuilder configuration)
        {
            configuration.AddJsonFile("appsettings.json", false, true); 
            return _configurationRoot = configuration.Build();
        }

        public override void Initialize(IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            base.Initialize(serviceProvider);
        }
    }
}
