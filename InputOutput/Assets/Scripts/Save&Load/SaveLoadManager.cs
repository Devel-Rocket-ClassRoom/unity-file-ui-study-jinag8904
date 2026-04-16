using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using SaveDataVC = SaveDataV3;

public static class SaveLoadManager
{
    public enum SaveMode { Text, Encrypted }
    public static SaveMode Mode { get; set; } = SaveMode.Text;

    private static readonly string SaveDirPath = $"{Path.Combine(Application.persistentDataPath, "Save")}";

    private static readonly string[] SaveFileNames =
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3"
    };

    public static int SaveDataVersion { get; } = 3;
    public static SaveDataVC Data { get; set; } = new();

    private static JsonSerializerSettings settings = new()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All
    };

    private static string GetSaveFilePath(int slot)
    {
        return GetSaveFilePath(slot, Mode);
    }

    private static string GetSaveFilePath(int slot, SaveMode mode)
    {
        var ext = mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirPath, $"{SaveFileNames[slot]}{ext}");
    }

    public static bool Save(int slot, SaveMode mode)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)   return false;

        try
        {
            if (!Directory.Exists(SaveDirPath)) Directory.CreateDirectory(SaveDirPath);

            var json = JsonConvert.SerializeObject(Data, settings);
            var path = GetSaveFilePath(0, mode);

            switch (Mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    Debug.Log(".json 파일 저장 완료");
                    break;

                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    Debug.Log(".dat 파일 저장 완료");
                    break;
            }

            return true;
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }

    public static bool Load(int slot, SaveMode mode)
    {
        if (slot < 0 || slot >= SaveFileNames.Length || !Directory.Exists(SaveDirPath)) return false;

        string path = GetSaveFilePath(0, mode);
        if (!File.Exists(path)) return false;

        try
        {
            string json = string.Empty;

            switch (Mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    Debug.Log(".json 파일 로드 완료");
                    break;

                case SaveMode.Encrypted:
                    json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    Debug.Log(".dat 파일 로드 완료");
                    break;
            }

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