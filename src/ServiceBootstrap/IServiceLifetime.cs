namespace ServiceBootstrap
{
    /// <summary>
    ///     Interface that defines the specific service lifetimes. 
    /// </summary>
    public interface IServiceLifetime
    {
        /// <summary>
        ///     Gets service input args
        /// </summary>
        string[] Args { get; }

        /// <summary>
        ///     Start the service lifetime method.
        /// </summary>
        /// <param name="args">Service start arguments.</param>
        /// <returns>Control code.</returns>
        int Start(string[] args);

        /// <summary>
        ///     Sets up the service lifetime.
        /// </summary>
        /// <param name="context">Service lifetime context.</param>
        void SetUp(object context);
    }
}
