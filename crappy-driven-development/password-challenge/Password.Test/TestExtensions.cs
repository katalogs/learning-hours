using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Password.Test;

public static class TestExtensions
{
    public static string GetResourceAsString(this Assembly assembly, string relativeResourcePath)
    {
        ArgumentNullException.ThrowIfNull(relativeResourcePath);
        var resourcePath = $"{Regex.Replace(assembly.ManifestModule.Name, @"\.(exe|dll)$", string.Empty, RegexOptions.IgnoreCase)}.{relativeResourcePath}";

        var stream = assembly.GetManifestResourceStream(resourcePath);
        if (stream == null)
            throw new ArgumentException($"The specified embedded resource {relativeResourcePath} is not found.");

        return new StreamReader(stream).ReadToEnd();
    }
}