using System;
using System.Collections.Generic;
using System.Reflection;

using Microsoft.Extensions.Hosting;

namespace ServiceBootstrap
{
    /// <summary>
    ///      A base class for initializing the service
    /// </summary>
    /// <typeparam name="TServiceBuilder">Service builder type.</typeparam>
    public abstract class ServiceBuilderBase<TServiceBuilder> where TServiceBuilder : ServiceBuilderBase<TServiceBuilder>, new()
    {
        #region Fields

        private static bool _setupWasAlreadyCalled;

        private Func<ServiceBootstrapper> _serviceFactory;
        private IServiceLifetime _serviceLifetime;

        #endregion

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="platform">Runtime Platform</param>
        /// <param name="platformServicesInitializer">Platform service builder.</param>
        /// <param name="domainAssemblyInitializer">Domain Assembly loader.</param>
        protected ServiceBuilderBase(IRuntimePlatform platform, Action<TServiceBuilder> platformServicesInitializer, Action<TServiceBuilder> domainAssemblyInitializer)
        {
            RuntimePlatform = platform;
            RuntimePlatformServicesInitializer = () => platformServicesInitializer(Self);
            DomainAssemblyInitializer = () => domainAssemblyInitializer(Self);
        }

        /// <summary>
        ///     Forces check setup logic to be executed
        /// </summary>
        protected virtual bool CheckSetup => true;

        /// <summary>
        ///     Domain Assembly initializer
        /// </summary>
        public Action DomainAssemblyInitializer { get; set; }

        /// <summary>
        ///     Gets or sets a method to call the initialize the runtime platform services (e. g. AssetLoader)
        /// </summary>
        public Action RuntimePlatformServicesInitializer { get; }

        /// <summary>
        ///     Gets or sets the <see cref="IRuntimePlatform"/> instance.
        /// </summary>
        public IRuntimePlatform RuntimePlatform { get; set; }

        /// <summary>
        ///     Gets the type of the Instance (even if it's not created yet)
        /// </summary>
        public Type ApplicationType { get; private set; }

        /// <summary>
        ///     Gets the <see cref="ServiceBootstrapper"/> instance being initialized.
        /// </summary>
        public ServiceBootstrapper Instance { get; private set; }
        
        /// <summary>
        ///     Gets Service host <see cref="IHost"/>
        /// </summary>
        public IHost ServiceHost { get; private set; }

        /// <summary>
        ///     Gets collection of domain assemblies
        /// </summary>
        public IList<Assembly> DomainAssemblies { get; set; }
        
        #region Static methods

        /// <summary>
        ///     Begin configuring an <see cref="ServiceBootstrapper"/>.
        /// </summary>
        /// <typeparam name="TService">The subclass of <see cref="ServiceBootstrapper"/> to configure.</typeparam>
        /// <returns>An <typeparamref name="TServiceBuilder"/> instance.</returns>
        public static TServiceBuilder Create<TService>() where TService : ServiceBootstrapper, new()
        {
            var serviceBuilder = new TServiceBuilder
            {
                ApplicationType = typeof(TService),
                _serviceFactory = () => new TService()
            };

            return serviceBuilder;
        }

        /// <summary>
        ///     Begin configuring an <see cref="ServiceBootstrapper"/>.
        /// </summary>
        /// <param name="serviceFactory">Factory function for <typeparamref name="TService"/>.</param>
        /// <typeparam name="TService">The subclass of <see cref="ServiceBootstrapper"/> to configure.</typeparam>
        /// <remarks><paramref name="serviceFactory"/> is useful for passing of dependencies to <typeparamref name="TService"/>.</remarks>
        /// <returns>An <typeparamref name="TServiceBuilder"/> instance.</returns>
        public static TServiceBuilder Create<TService>(Func<TService> serviceFactory) where TService : ServiceBootstrapper
        {
            var serviceBuilder = new TServiceBuilder
            {
                ApplicationType = typeof(TService),
                _serviceFactory = serviceFactory
            };
            return serviceBuilder;
        }

        #endregion
        
        protected TServiceBuilder Self => (TServiceBuilder)this;

        /// <summary>
        ///     Sets up the platform-specific services for the application, but does not run it.
        /// </summary>
        /// <returns></returns>
        public TServiceBuilder SetupWithoutStarting()
        {
            InternalSetupService();

            return Self;
        }

        /// <summary>
        ///     Sets up the platform-specific services for the application and initialized it with a particular lifetime, but does not run it.
        /// </summary>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public TServiceBuilder SetupWithLifetime(IServiceLifetime lifetime)
        {
            _serviceLifetime = lifetime;

            InternalSetupService();

            return Self;
        }

        /// <summary>
        ///    Gets AfterPlatformServicesSetupCallback
        /// </summary>
        public Action<TServiceBuilder> AfterPlatformServicesSetupCallback { get; } = builder => { };

        #region Privte methods
        
        /// <summary>
        ///     Executes the initialization protocol.
        /// </summary>
        private void InternalSetupService()
        {
            if (RuntimePlatformServicesInitializer == default)
            {
                throw new InvalidOperationException("No runtime platform services configured.");
            }

            if (_setupWasAlreadyCalled && CheckSetup)
            {
                throw new InvalidOperationException("Setup was already called on one of ServiceBuilder instances");
            }
            
            _setupWasAlreadyCalled = true;
            var hostBuilder = new HostBuilder();

            //1. Scan the environment 
            RuntimePlatformServicesInitializer();

            //2. Fetch information about the domain assemblies
            DomainAssemblyInitializer();

            //3. Create service bootstrapper instance
            Instance = _serviceFactory();
            
            //4. Register the domain assemblies within the service bootstrap instance 
            Instance.RegisterDomainAssemblies(DomainAssemblies);

            //5. Register service dependencies
            Instance.RegisterComponents(hostBuilder);

            //6. Build the service host instance
            ServiceHost =  hostBuilder.Build();

            //7. Associate the lifetime
            Instance.ApplicationLifetime = _serviceLifetime;

            //8. Initialize service.
            Instance.Initialize(ServiceHost.Services);
            
            //9. Call the callback after the service has been initialized
            AfterPlatformServicesSetupCallback(Self);
            
            //10. Signal that the sequence is complete 
            Instance.OnInitializationCompleted();
        }
        #endregion
    }
}
