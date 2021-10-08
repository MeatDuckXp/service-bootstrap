using System;

namespace ServiceBootstrap
{
    /// <summary>
    ///     Simple service builder implementation
    /// </summary>
    public class ServiceBuilder : ServiceBuilderBase<ServiceBuilder>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public ServiceBuilder() : base(new StandardServiceRuntimePlatform(), builder => StandardServiceRuntimePlatformServices.Register(builder.ApplicationType.Assembly), LoadAssemblyInformation())
        {

        }

        private static Action<ServiceBuilder> LoadAssemblyInformation()
        {
            return builder =>
            { 
                builder.DomainAssemblies = DomainAssemblyLoader.GetAssemblies();
            };
        }
    }
}