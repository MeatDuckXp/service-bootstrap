using System.Threading.Tasks;

namespace ServiceBootstrap
{
    /// <summary>
    ///     Extension methods related to the service builder
    /// </summary>
    public static class ServiceConsoleLifetimeExtension
    {
        /// <summary>
        ///     Starts the service with the console life time setting.
        /// </summary>
        /// <typeparam name="TServiceBuilder">Service builder type.</typeparam>
        /// <param name="builder">Service builder instance.</param>
        /// <param name="args">Service input arguments.</param>
        /// <returns>Control value.</returns>
        public static int StartWithConsoleLifetime<TServiceBuilder>(this TServiceBuilder builder, string[] args) where TServiceBuilder : ServiceBuilderBase<TServiceBuilder>, new()
        {
            var lifetime = new ConsoleServiceLifeTime
            {
                Args = args
            };

            builder.SetupWithLifetime(lifetime);

            lifetime.SetHost(builder.ServiceHost);

            return lifetime.Start(args);
        }


        /// <summary>
        ///     Starts the service with the console life time setting asynchronously.
        /// </summary>
        /// <typeparam name="TServiceBuilder">Service builder type.</typeparam>
        /// <param name="builder">Service builder instance.</param>
        /// <param name="args">Service input arguments.</param>
        /// <returns>Control value.</returns>
        public static async Task<int> StartWithConsoleLifetimeAsync<TServiceBuilder>(this TServiceBuilder builder, string[] args) where TServiceBuilder : ServiceBuilderBase<TServiceBuilder>, new()
        {
            var lifetime = new ConsoleServiceLifeTime
            {
                Args = args
            };

            builder.SetupWithLifetime(lifetime);

            lifetime.SetHost(builder.ServiceHost);

            return await lifetime.StartAsync(args);
        }
    }
}