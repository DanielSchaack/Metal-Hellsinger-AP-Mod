using System.Collections.Generic;
using System.IO;
using System.Text;
using Randomizer;

public static class ItemExporter
{
    public static void ExportItemTable(
        Dictionary<long, ItemData> itemDataById, 
        string outputPath)
    {

        string dir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        using (var writer = new StreamWriter(outputPath, false, Encoding.UTF8, bufferSize: 65536))
        {
            writer.WriteLine("item_table: dict[str, ItemData] = {");

            foreach (var kvp in itemDataById)
            {
                ItemData item = kvp.Value;

                string escapedName = EscapePythonString(item.Name);
                string groupName = EscapePythonString(item.Type.ToString());
                string classification = item.Classification.ToString();
                int requiredNum = item.QuantityToGive;

                writer.WriteLine(
                    $"    \"{escapedName}\": ItemData(\"{escapedName}\", {item.ArchipelagoId}, \"{groupName}\", ItemClassification.{classification}, {requiredNum}),"
                );
            }

            writer.WriteLine("}");
        }
    }

    private static string EscapePythonString(string value)
    {
        if (string.IsNullOrEmpty(value)) return "";

        return value
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r")
            .Replace("\t", "\\t");
    }
}
