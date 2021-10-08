namespace ServiceBootstrap
{
    /// <summary>
    ///     This class defines the runtime information that can be accessed by the services.
    /// </summary>
    public struct RuntimePlatformInformation
    {
        /// <summary>
        ///     Gets or Sets OperatingSystem information
        /// </summary>
        public OperatingSystemType OperatingSystem { get; set; }
    }
}
