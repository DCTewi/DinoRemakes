using Stride.Core;

using System.IO;
using System.Text.Json;

namespace DinoRemakes.Core.Models
{
    public class SaveData
    {
        public int BestScore { get; set; } = 0;
    }

    public static class SaveExtensions
    {
        private static readonly string _SaveDir = Path.Combine(PlatformFolders.ApplicationLocalDirectory, "data");
        private static readonly string _SaveFile = Path.Combine([PlatformFolders.ApplicationLocalDirectory, "data", "save.dat"]);

        public static SaveData Load(this SaveData save)
        {
            bool loadSuccess = false;

            if (File.Exists(_SaveFile))
            {
                var content = File.ReadAllText(_SaveFile);
                try
                {
                    save = JsonSerializer.Deserialize<SaveData>(content, JsonContext.Default.SaveData);
                    loadSuccess = true;
                }
                catch { }
            }

            if (!loadSuccess)
            {
                save = new SaveData().Save();
                return save;
            }

            return save;
        }

        public static SaveData Save(this SaveData save)
        {
            var content = JsonSerializer.Serialize(save, JsonContext.Default.SaveData);

            if (!Directory.Exists(_SaveDir))
            {
                Directory.CreateDirectory(_SaveDir);
            }

            File.WriteAllText(_SaveFile, content);

            return save;
        }
    }
}
