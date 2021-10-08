namespace ServiceBootstrap
{
    /// <summary>
    ///     General service life time definitions. This can be currently seen as a placeholder, but ot be expanded in near future.
    /// </summary>
    public class ServiceLifetime : IServiceLifetime
    {
        /// <summary>
        ///     Gets service input args
        /// </summary>
        public string[] Args { get; private set;}

        /// <summary>
        ///     Start the service lifetime method.
        /// </summary>
        /// <param name="args">Service start arguments.</param>
        /// <returns>Control code.</returns>
        public virtual int Start(string[] args)
        {
            return default;
        }

        /// <summary>
        ///     Sets up the service lifetime.
        /// </summary>
        /// <param name="context">Service lifetime context.</param>
        public void SetUp(object context)
        {

        }
    }
}