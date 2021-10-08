using System;
using System.Runtime.InteropServices;

namespace ServiceBootstrap
{
    /// <summary>
    ///     Standard service runtime platform 
    /// </summary>
    public class StandardServiceRuntimePlatform : IRuntimePlatform
    {
        /// <summary>
        ///     Gets Runtime information
        /// </summary>
        private static readonly Lazy<RuntimePlatformInformation> Info = new Lazy<RuntimePlatformInformation>(() =>
        {
            OperatingSystemType operatingSystem;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                operatingSystem = OperatingSystemType.Linux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                operatingSystem = OperatingSystemType.WinNT;
            }
            else
            {
                throw new Exception("Unknown OS platform " + RuntimeInformation.OSDescription);
            }

            var runtimeInfo = new RuntimePlatformInformation
            {
                OperatingSystem = operatingSystem

            };

            return runtimeInfo;
        });

        /// <summary>
        ///     Gets runtime information
        /// </summary>
        /// <returns>RuntimePlatformInformation</returns>
        public RuntimePlatformInformation GetRuntimeInformation()
        {
            var info = Info.Value;
            return info;
        }
    }
}
