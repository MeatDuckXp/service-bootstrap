namespace ServiceBootstrap
{
    /// <summary>
    ///     This interface defines the runtime platform lookup operations.
    /// </summary>
    public interface IRuntimePlatform
    { 
        /// <summary>
        ///     Gets the runtime information.
        /// </summary>
        /// <returns>RuntimePlatformInformation</returns>
        RuntimePlatformInformation GetRuntimeInformation();
    }
}
