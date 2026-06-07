using System.Text.Json;
using SmartDisplay.Shared.Models.Ujin;

namespace SmartDisplay.ScreenClient.Services;

public static class UjinApiParser
{
    public static List<UjinNewsDto> ParseNews(string content)
    {
        try
        {
            using var document = JsonDocument.Parse(content);
            if (document.RootElement.ValueKind != JsonValueKind.Array)
            {
                return ScreenFallbackData.CreateNews();
            }

            var result = new List<UjinNewsDto>();
            foreach (var item in document.RootElement.EnumerateArray())
            {
                if (item.ValueKind != JsonValueKind.Object)
                {
                    continue;
                }

                result.Add(new UjinNewsDto
                {
                    Id = ReadInt(item, "id"),
                    Date = ReadString(item, "date", "created_at", "published_at"),
                    Title = ReadString(item, "title", "name", "subject"),
                    Text = ReadString(item, "text", "body", "content", "description"),
                    Images = ReadStringList(item, "images")
                });
            }

            return result.Count > 0 ? result : ScreenFallbackData.CreateNews();
        }
        catch
        {
            return ScreenFallbackData.CreateNews();
        }
    }

    public static ParkingFreeResponse ParseParking(string content)
    {
        try
        {
            using var document = JsonDocument.Parse(content);
            var root = document.RootElement;

            if (root.ValueKind == JsonValueKind.Object)
            {
                var total = ReadInt(root, "total_free", "free_count", "unassigned", "count");
                var zones = ReadZones(root);
                return new ParkingFreeResponse
                {
                    TotalFree = total > 0 ? total : zones.Sum(zone => zone.FreeCount),
                    Zones = zones
                };
            }

            if (root.ValueKind == JsonValueKind.Array)
            {
                var zones = new List<ParkingZoneDto>();
                var index = 1;

                foreach (var item in root.EnumerateArray())
                {
                    if (item.ValueKind != JsonValueKind.Object)
                    {
                        continue;
                    }

                    var count = ReadInt(item, "free_count", "total_free", "unassigned", "count");
                    var name = ReadString(item, "name", "title", "zone", "zone_name", "parking_name");
                    zones.Add(new ParkingZoneDto
                    {
                        Name = string.IsNullOrWhiteSpace(name) ? $"Зона {index}" : name,
                        FreeCount = count
                    });
                    index++;
                }

                return new ParkingFreeResponse
                {
                    TotalFree = zones.Sum(zone => zone.FreeCount),
                    Zones = zones
                };
            }
        }
        catch
        {
            // ignored, fallback below
        }

        return ScreenFallbackData.CreateParking();
    }

    public static StorageFreeResponse ParseStorage(string content)
    {
        try
        {
            using var document = JsonDocument.Parse(content);
            var root = document.RootElement;

            if (root.ValueKind == JsonValueKind.Object)
            {
                return new StorageFreeResponse
                {
                    TotalFree = ReadInt(root, "total_free", "free_count", "unassigned", "count")
                };
            }

            if (root.ValueKind == JsonValueKind.Array)
            {
                var total = 0;
                foreach (var item in root.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.Object)
                    {
                        total += ReadInt(item, "total_free", "free_count", "unassigned", "count");
                    }
                }

                return new StorageFreeResponse { TotalFree = total };
            }
        }
        catch
        {
            // ignored, fallback below
        }

        return ScreenFallbackData.CreateStorage();
    }

    private static List<ParkingZoneDto> ReadZones(JsonElement root)
    {
        if (!root.TryGetProperty("zones", out var zonesElement) || zonesElement.ValueKind != JsonValueKind.Array)
        {
            return new List<ParkingZoneDto>();
        }

        var zones = new List<ParkingZoneDto>();
        var index = 1;
        foreach (var zoneElement in zonesElement.EnumerateArray())
        {
            if (zoneElement.ValueKind != JsonValueKind.Object)
            {
                continue;
            }

            var name = ReadString(zoneElement, "name", "title", "zone", "zone_name");
            zones.Add(new ParkingZoneDto
            {
                Name = string.IsNullOrWhiteSpace(name) ? $"Зона {index}" : name,
                FreeCount = ReadInt(zoneElement, "free_count", "total_free", "unassigned", "count")
            });
            index++;
        }

        return zones;
    }

    private static int ReadInt(JsonElement item, params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
        {
            if (!item.TryGetProperty(propertyName, out var property))
            {
                continue;
            }

            if (property.ValueKind == JsonValueKind.Number && property.TryGetInt32(out var number))
            {
                return number;
            }

            if (property.ValueKind == JsonValueKind.String && int.TryParse(property.GetString(), out var parsed))
            {
                return parsed;
            }
        }

        return 0;
    }

    private static string ReadString(JsonElement item, params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
        {
            if (item.TryGetProperty(propertyName, out var property) && property.ValueKind == JsonValueKind.String)
            {
                return property.GetString() ?? string.Empty;
            }
        }

        return string.Empty;
    }

    private static List<string> ReadStringList(JsonElement item, string propertyName)
    {
        if (!item.TryGetProperty(propertyName, out var property) || property.ValueKind != JsonValueKind.Array)
        {
            return new List<string>();
        }

        return property.EnumerateArray()
            .Where(element => element.ValueKind == JsonValueKind.String)
            .Select(element => element.GetString() ?? string.Empty)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .ToList();
    }
}
