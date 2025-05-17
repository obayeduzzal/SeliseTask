using System.IO;

namespace TTM.Core.Shared.Helpers.Json;

public static class JsonDataReader
{
    public static T ReadFromJson<T>(string fileName, string[]? additionalDirectoryPath = null)
    {
        string folderPath;
        if (additionalDirectoryPath != null)
        {
            folderPath = Path.Combine(additionalDirectoryPath!);
        }
        else
        {
            folderPath = Path.Combine(Environment.CurrentDirectory, "TestData");
        }

        foreach (string file in Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories))
        {
            FileInfo fileInfo = new(file);
            if (fileInfo.Name.Equals($"{fileName}.json", StringComparison.OrdinalIgnoreCase))
            {
                string json = File.ReadAllText(file);
                return json.Deserialize<T>(isCamelCase: true);
            }
        }

        return default!;
    }
}
