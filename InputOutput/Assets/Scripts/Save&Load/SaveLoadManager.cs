using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using SaveDataVC = SaveDataV3;

public static class SaveLoadManager
{
    private static readonly string SaveDirPath = $"{Path.Combine(Application.persistentDataPath, "Save")}";

    private static readonly string[] SaveFileNames =
    {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json"
    };

    public static int SaveDataVersion { get; } = 3;
    public static SaveDataVC Data { get; set; } = new();

    private static JsonSerializerSettings settings = new()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All
    };

    public static bool Save(int slot = 0)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)   return false;

        try
        {
            if (!Directory.Exists(SaveDirPath)) Directory.CreateDirectory(SaveDirPath);

            var json = JsonConvert.SerializeObject(Data, settings);
            var path = Path.Combine(SaveDirPath, SaveFileNames[slot]);
            File.WriteAllText(path, json);

            return true;
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }

    public static bool Load(int slot = 0)
    {
        if (slot < 0 || slot >= SaveFileNames.Length || !Directory.Exists(SaveDirPath)) return false;

        var path = Path.Combine(SaveDirPath, SaveFileNames[slot]);
        if (!File.Exists(path)) return false;

        try
        {
            var json = File.ReadAllText(path);
            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);

            while (saveData.Version < SaveDataVersion)
            {
                saveData = saveData.VersionUp();
            }

            Data = saveData as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }
    }
}