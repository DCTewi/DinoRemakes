using DinoRemakes.Core.SourceGen;

using Godot;

using System.Text.Json;

namespace DinoRemakes.Core.Save;

public static class SaveExtensions
{
    private static readonly string _SaveDir = "user://data/";
    private static readonly string _SavePath = "user://data/save.dat";

    public static SaveData Load(ref this SaveData data)
    {
        bool loadSuccess = false;

        if (FileAccess.FileExists(_SavePath))
        {
            using var file = FileAccess.Open(_SavePath, FileAccess.ModeFlags.Read);
            var dataStr = file?.GetAsText(skipCr: true) ?? string.Empty;

            try
            {
                data = JsonSerializer.Deserialize(dataStr, JsonContext.Default.SaveData);
                loadSuccess = true;
            }
            catch { }
        }

        if (!loadSuccess)
        {
            data = new SaveData().Save();
        }

        return data;
    }

    public static SaveData Save(this SaveData data)
    {
        var dataStr = JsonSerializer.Serialize(data, JsonContext.Default.SaveData);

        if (!DirAccess.DirExistsAbsolute(_SaveDir))
        {
            DirAccess.MakeDirRecursiveAbsolute(_SaveDir);
        }

        using var file = FileAccess.Open(_SavePath, FileAccess.ModeFlags.Write);
        file?.StoreString(dataStr);

        if (file is null)
        {
            GD.Print(FileAccess.GetOpenError());
        }

        return data;
    }
}
