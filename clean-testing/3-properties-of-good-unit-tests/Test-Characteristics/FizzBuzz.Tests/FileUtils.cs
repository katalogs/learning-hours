using System;
using static System.IO.File;

namespace FizzBuzz.Tests;

public static class FileUtils
{
    public static void AppendToFile(string fileName, string content)
    {
        if (!Exists(fileName)) WriteAllText(fileName, content);
        else AppendAllText(fileName, $"{Environment.NewLine}{content}");
    }

    public static void DeleteFile(string fileName)
        => Delete(fileName);

    public static int CountLines(string fileName) =>
        ReadAllLines(fileName)
            .Length;
}