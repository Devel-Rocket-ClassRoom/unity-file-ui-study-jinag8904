using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{
    public enum Difficulty { Easy, Normal, Hard }
    public Difficulty difficulty = Difficulty.Easy;

    public Toggle[] toggles;

    public int selected;

    public Button cancelButton;
    public Button applyButton;

    private string path;

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);

        cancelButton.onClick.AddListener(OnCancel);
        applyButton.onClick.AddListener(OnApply);

        path = Path.Combine(Application.persistentDataPath, "DragonSweeper", "difficulty.json");
    }

    public override void Open()
    {
        base.Open();

        var folderPath = Path.Combine(Application.persistentDataPath, "DragonSweeper");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        try
        {
            var json = File.ReadAllText(path);

            difficulty = JsonConvert.DeserializeObject<Difficulty>(json);

            switch (difficulty)
            {
                case Difficulty.Easy:
                    selected = 0;
                    break;
                case Difficulty.Normal:
                    selected = 1;
                    break;
                case Difficulty.Hard:
                    selected = 2;
                    break;
            }
        }

        catch
        {
            Debug.Log("불러오기 중 예외 발생");
            selected = 0;
        }

        toggles[selected].isOn = true;
    }

    public void OnEasy(bool active)
    {
        difficulty = Difficulty.Easy;
    }

    public void OnNormal(bool active)
    {
        difficulty = Difficulty.Normal;
    }

    public void OnHard(bool active)
    {
        difficulty = Difficulty.Hard;
    }

    public void OnCancel()
    {
        windowManager.Open(0);
    }

    public void OnApply()
    {
        var folderPath = Path.Combine(Application.persistentDataPath, "DragonSweeper");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        try
        {
            var json = JsonConvert.SerializeObject(difficulty);
            File.WriteAllText(path, json);
        }

        catch
        {
            Debug.Log("저장 중 예외 발생");
        }

        windowManager.Open(0);
    }
}