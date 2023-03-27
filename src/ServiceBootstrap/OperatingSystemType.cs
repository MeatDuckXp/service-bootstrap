namespace ServiceBootstrap
{
    /// <summary>
    ///     Defines the various operating system types
    /// </summary>
    public enum OperatingSystemType
    {
        /// <summary>
        ///     Default value, to cover the scenario in case things are not set.
        /// </summary>
        Unknown,

        /// <summary>
        ///     Windows NT
        /// </summary>
        WinNT,

        /// <summary>
        ///     Linux distribution
        /// </summary>
        Linux, 

        /// <summary>
        ///     Future plan - emulating the environments to automate testing. Its a plan, no work made yet.
        /// </summary>
        Emulator
    }
}
