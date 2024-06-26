using System.Reflection;

namespace PropertyConfig;

internal static class Constants
{
    internal const string RootElementKey = "properties";
    
    internal const string CommentElementKey = "comment";
    
    internal const string EntryElementKey = "entry";
    
    internal const string KeyAttributeKey = "key";
    
    internal static string LibVersion => GetAssemblyVersion();
    
    private static string GetAssemblyVersion()
    {
        var assembly = typeof(Properties).GetTypeInfo().Assembly;
        return assembly.GetName().Version.ToString();
    }
}