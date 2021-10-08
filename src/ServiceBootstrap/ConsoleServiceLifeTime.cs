using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ServiceBootstrap
{
    /// <summary>
    ///     This class represents the life time settings and related set up logic for the console based application. PLease
    ///     note, all of our services that consume messages from the message queue are running in console lifetime.
    /// </summary>
    public class ConsoleServiceLifeTime : IConsoleServiceLifeTime
    {
        /// <summary>
        ///     Gets Host
        /// </summary>
        public IHost Host { get; private set; }

        /// <summary>
        ///     Gets service input args
        /// </summary>
        public string[] Args { get; set; }
        
        /// <summary>
        ///     Start the service lifetime method.
        /// </summary>
        /// <param name="args">Service start arguments.</param>
        /// <returns>Control code.</returns>
        public int Start(string[] args)
        {
            ValidateHostInstance(Host);

            Args = args;

            Host.RunAsync();

            return default;
        }

        /// <summary>
        ///     Start the service lifetime method.
        /// </summary>
        /// <param name="args">Service start arguments.</param>
        /// <returns>Control code.</returns>
        public async Task<int> StartAsync(string[] args)
        {
            ValidateHostInstance(Host);

            Args = args;
            
            await Host.RunAsync();

            return default;
        }

        /// <summary>
        ///     Sets up the service lifetime.
        /// </summary>
        /// <param name="context">Service lifetime context.</param>
        public void SetUp(object context)
        {
            if (context == default)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var hostBuilder = (IHostBuilder) context;

            hostBuilder.UseConsoleLifetime();
        }

        /// <summary>
        ///     Sets the host instance to the console lifetime object.
        /// </summary>
        /// <param name="host">Host instance.</param>
        public void SetHost(IHost host)
        {
            if (host == default)
            {
                throw new ArgumentNullException(nameof(host));
            }

            Host = host;
        }

        #region Private methods

        /// <summary>
        ///     Validates that the host instance is set. If it is not set, exception will be thrown.
        /// </summary>
        /// <param name="host">Host instance.</param>
        private void ValidateHostInstance(IHost host)
        {
            if (host == default)
            {
                throw new InvalidOperationException("Host instance in not set. Check that the method SetHost() is invoked.");
            }
        }

        #endregion
    }
}