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
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json"
    };

    private static readonly string[] EncryptedSaveFileNames =
    {
        "SaveAuto.dat",
        "Save1.dat",
        "Save2.dat",
        "Save3.dat"
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

            var text = JsonConvert.SerializeObject(Data, settings);
            string path;

            switch (Mode)
            {
                case SaveMode.Text:
                    path = Path.Combine(SaveDirPath, SaveFileNames[slot]);
                    File.WriteAllText(path, text);
                    break;
                case SaveMode.Encrypted:
                    var encrypted = CryptoUtil.Encrypt(text);
                    path = Path.Combine(SaveDirPath, EncryptedSaveFileNames[slot]);
                    File.WriteAllBytes(path, encrypted);
                    Debug.Log("암호화 완료");
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

    public static bool Load(int slot = 0)
    {
        if (slot < 0 || slot >= SaveFileNames.Length || !Directory.Exists(SaveDirPath)) return false;

        string path = string.Empty;
        string saveText = string.Empty;

        try
        {
            switch (Mode)
            {
                case SaveMode.Text:
                    path = Path.Combine(SaveDirPath, SaveFileNames[slot]);
                    if (!File.Exists(path)) return false;

                    saveText = File.ReadAllText(path);
                    break;

                case SaveMode.Encrypted:
                    path = Path.Combine(SaveDirPath, EncryptedSaveFileNames[slot]);
                    if (!File.Exists(path)) return false;

                    var encrypted = File.ReadAllBytes(path);
                    saveText = CryptoUtil.Decrypt(encrypted);
                    Debug.Log("복호화 완료");
                    break;
            }

            var saveData = JsonConvert.DeserializeObject<SaveData>(saveText, settings);

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