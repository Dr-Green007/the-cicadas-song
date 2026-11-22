using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace thecicadassong
{
    public static class SaveManager
    {
        private static readonly string Folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TheCicadasSong", "saves");

        private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

        private static void EnsureFolder()
        {
            if (!Directory.Exists(Folder)) Directory.CreateDirectory(Folder);
        }

        public static string PathFor(string name)
        {
            var safe = string.Join("_", name.Split(Path.GetInvalidFileNameChars()));
            return Path.Combine(Folder, safe + ".json");
        }

        public static bool Save(GameState data, out string error)
        {
            error = "";
            try
            {
                EnsureFolder();
                data.SavedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var json = JsonSerializer.Serialize(data, Options);
                File.WriteAllText(PathFor(data.PlayerName), json);
                return true;
            }
            catch (Exception ex) { error = ex.Message; return false; }
        }

        public static GameState? Load(string name, out string error)
        {
            error = "";
            try
            {
                var path = PathFor(name);
                if (!File.Exists(path)) { error = "Сохранение не найдено."; return null; }
                var json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<GameState>(json);
                if (data is null) { error = "Файл повреждён."; return null; }
                return data;
            }
            catch (Exception ex) { error = ex.Message; return null; }
        }

        public static bool Exists(string name) => File.Exists(PathFor(name));

        public static List<string> ListSaves()
        {
            var result = new List<string>();
            try
            {
                if (!Directory.Exists(Folder)) return result;
                foreach (var f in Directory.GetFiles(Folder, "*.json"))
                    result.Add(Path.GetFileNameWithoutExtension(f));
            }
            catch { }
            return result;
        }
    }
}