using System.Collections.Generic;
using System.IO;
using System.Text;
using static Randomizer.Locations;

public static class LocationMappingExporter
{
    public static void ExportLocationRegionMapping(
        Dictionary<string, Location> locationDataByName, 
        string outputPath)
    {

        var regionGroups = new Dictionary<string, List<Location>>();

        foreach (var kvp in locationDataByName)
        {
            Location loc = kvp.Value;
            string regionName = GetRegionName(loc.Zone, loc.Arena);

            if (!regionGroups.TryGetValue(regionName, out var list))
            {
                list = new List<Location>();
                regionGroups[regionName] = list;
            }
            list.Add(loc);
        }

        string dir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        using (var writer = new StreamWriter(outputPath, false, Encoding.UTF8, bufferSize: 65536))
        {
            writer.WriteLine("location_region_mapping: dict[str, dict[str, LocationData]] = {");

            int regionIndex = 0;
            int totalRegions = regionGroups.Count;

            foreach (var regionKvp in regionGroups)
            {
                regionIndex++;
                string escapedRegion = EscapePythonString(regionKvp.Key);
                writer.WriteLine($"    \"{escapedRegion}\": {{");

                var locations = regionKvp.Value;
                for (int i = 0; i < locations.Count; i++)
                {
                    var loc = locations[i];
                    string escapedDesc = EscapePythonString(loc.Description);
                    string ltype = loc.LocationType.ToString();

                    writer.WriteLine($"        \"{escapedDesc}\": LocationData({loc.ArchipelagoId}, \"{ltype}\"),");
                }

                string trailingComma = (regionIndex < totalRegions) ? "," : ",";
                writer.WriteLine($"    }}{trailingComma}");
            }

            writer.WriteLine("}");
        }
    }

    private static string GetRegionName(EZone zone, EArena arena)
    {
        if (zone == EZone.Global && arena == EArena.Global)
        {
            return "Global";
        }

        if (zone == EZone.Tutorial && arena == EArena.Tutorial)
        {
            return "Tutorial";
        }

        return $"{zone} {arena}";
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
