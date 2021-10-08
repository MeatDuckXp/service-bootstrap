using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceBootstrap
{
    /// <summary>
    ///     Fetches the information about the domain assemblies
    /// </summary>
    public class DomainAssemblyLoader
    {
        private static IList<Assembly> _assemblies;

        /// <summary>
        ///     Returns a collection of assemblies loaded in the current application domain.
        /// </summary>
        /// <returns></returns>
        public static IList<Assembly> GetAssemblies()
        {
            if (_assemblies == default)
            {
                _assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            }
            return _assemblies;
        }
    }
}