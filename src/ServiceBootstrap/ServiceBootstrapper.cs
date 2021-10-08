using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ServiceBootstrap
{
    /// <summary>
    ///     Defines the service bootstrapper base class.
    /// </summary>
    public abstract class ServiceBootstrapper
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        protected ServiceBootstrapper()
        {
            Name = "Service base bootstrapper";
        }

        /// <summary>
        ///     Gets Name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     Gets or Sets ApplicationLifetime
        /// </summary>
        public IServiceLifetime ApplicationLifetime { get; set; }

        /// <summary>
        ///     Gets or Sets ServiceConfiguration
        /// </summary>
        public object ServiceConfiguration { get; set; }

        /// <summary>
        ///     Gets or Sets DomainAssemblies
        /// </summary>
        public IList<Assembly> DomainAssemblies { get; set; }

        /// <summary>
        ///     Executes the logic to register the necessary components for the service.
        /// </summary>
        /// <param name="hostBuilder">HostBuilder</param>
        public void RegisterComponents(HostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((hostContext, configApp) =>
            {
                ServiceConfiguration = LoadConfiguration(hostContext.HostingEnvironment, configApp);
            });

            hostBuilder.ConfigureLogging((hostBuilderContext, loggingBuilder) =>
            {
                ConfigureLogging(loggingBuilder, hostBuilderContext.Configuration);
            });

            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                RegisterServices(hostContext, services, ServiceConfiguration, DomainAssemblies);
            });
        }

        /// <summary>
        ///     Registers the domain assemblies within the bootstrapper 
        /// </summary>
        /// <param name="domainAssemblies"></param>
        public void RegisterDomainAssemblies(IList<Assembly> domainAssemblies)
        {
            //No checking of the input at this point.
            DomainAssemblies = domainAssemblies;
        }

        #region Abstract methods

        /// <summary>
        ///     Registers the necessary service references for DI
        /// </summary>
        /// <param name="builderContext">Host builder context.</param>
        /// <param name="serviceCollection">Service collection.</param>
        /// <param name="configuration">Configuration object.</param>
        /// <param name="domainAssemblies">Collection od domain assemblies.</param>
        public abstract void RegisterServices(HostBuilderContext builderContext, IServiceCollection serviceCollection, object configuration, IList<Assembly> domainAssemblies = null);

        /// <summary>
        ///     Configures the service logging strategy.
        /// </summary>
        /// <param name="loggingBuilder">Logging builder.</param>
        /// <param name="configuration">Configuration object.</param>
        public abstract void ConfigureLogging(ILoggingBuilder loggingBuilder, IConfiguration configuration);

        /// <summary>
        ///     Sets up the necessary service configuration.
        /// </summary>
        /// <param name="hostingEnvironment">Host environment.</param>
        /// <param name="configuration">Configuration builder.</param>
        /// <returns>Configuration object.</returns>
        public abstract object LoadConfiguration(IHostEnvironment hostingEnvironment, IConfigurationBuilder configuration);

        #endregion

        #region Virtual methods

        /// <summary>
        ///     Performs the initialization sequence.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public virtual void Initialize(IServiceProvider serviceProvider)
        {
        }

        /// <summary>
        ///     Call back method, called once the initialization sequence is completed.
        /// </summary>
        public virtual void OnInitializationCompleted()
        {
        }

        #endregion
    }
}