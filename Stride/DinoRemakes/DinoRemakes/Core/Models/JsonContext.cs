using System.Text.Json.Serialization;

namespace DinoRemakes.Core.Models
{
    [JsonSerializable(typeof(SaveData))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        AllowTrailingCommas = true,
        UseStringEnumConverter = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = false)]
    internal partial class JsonContext : JsonSerializerContext { }
}
