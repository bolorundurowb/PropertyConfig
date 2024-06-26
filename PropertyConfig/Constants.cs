using System.Reflection;

namespace PropertyConfig;

internal static class Constants
{
    internal static string LibVersion => GetAssemblyVersion();
    
    private static string GetAssemblyVersion()
    {
        var assembly = typeof(Properties).GetTypeInfo().Assembly;
        return assembly.GetName().Version.ToString();
    }
}