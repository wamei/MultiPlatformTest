using System;
using System.Text.RegularExpressions;

namespace MultiPlatformTest.Core
{
    public class VersionClassFactory
    {
        static public object Create(string className)
        {
            return Create(className, Models.Version.Current);
        }
        static public object Create(string className, Models.Version version)
        {
            var nsVersion = Regex.Replace(version.Value, "\\.", "_");
            var type = Type.GetType($"{typeof(App).Namespace}.Version_{nsVersion}.{className}");
            return Activator.CreateInstance(type);
        }
    }
}
