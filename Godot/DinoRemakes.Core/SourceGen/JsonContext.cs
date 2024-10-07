using DinoRemakes.Core.Save;

using System.Text.Json.Serialization;

namespace DinoRemakes.Core.SourceGen;

[JsonSerializable(typeof(SaveData))]
[JsonSourceGenerationOptions(
    WriteIndented = true,
    AllowTrailingCommas = true,
    UseStringEnumConverter = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    PropertyNameCaseInsensitive = false)]
public partial class JsonContext : JsonSerializerContext { }
