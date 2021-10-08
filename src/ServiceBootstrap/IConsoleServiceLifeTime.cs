using Microsoft.Extensions.Hosting;

namespace ServiceBootstrap
{
    /// <summary>
    ///     This interface defines the console lifetime specific operations.
    /// </summary>
    public interface IConsoleServiceLifeTime : IServiceLifetime
    {
        /// <summary>
        ///     Sets the host instance to the console lifetime object.
        /// </summary>
        /// <param name="host">Host instance.</param>
        void SetHost(IHost host);
    }
}