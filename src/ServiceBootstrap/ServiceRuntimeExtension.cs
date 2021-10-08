using System;

namespace ServiceBootstrap
{
    /// <summary>
    ///     Service runtime extension methods.
    /// </summary>
    public static class ServiceRuntimeExtension
    {
        /// <summary>
        ///     Executed the logic that detects the runtime detections.
        /// </summary>
        /// <typeparam name="TServiceBuilder">Service builder type.</typeparam>
        /// <param name="builder">Builder instance.</param>
        /// <returns>Service builder instance.</returns>
        public static TServiceBuilder UsePlatformDetect<TServiceBuilder>(this TServiceBuilder builder) where TServiceBuilder : ServiceBuilderBase<TServiceBuilder>, new()
        {
            var osType = builder.RuntimePlatform.GetRuntimeInformation().OperatingSystem;

            switch (osType)
            {
                case OperatingSystemType.Unknown:
                    break;
                case OperatingSystemType.WinNT:
                    break;
                case OperatingSystemType.Linux:
                    break;
                case OperatingSystemType.Emulator:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return builder;
        }

        /// <summary>
        ///     Executed the logic that is setting the service behavior to development mode.
        /// </summary>
        /// <typeparam name="TServiceBuilder">Service builder type.</typeparam>
        /// <param name="builder">Builder instance.</param>
        /// <returns>Service builder instance.</returns>
        public static TServiceBuilder UseDevelopmentMode<TServiceBuilder>(this TServiceBuilder builder) where TServiceBuilder : ServiceBuilderBase<TServiceBuilder>, new()
        {

#if RELEASE
    throw new InvalidOperationException("FATAL ERROR. Unsupported operation mode detected in release build configuration.");
#endif
            //set to dev mode

            return builder;
        }
    }
}