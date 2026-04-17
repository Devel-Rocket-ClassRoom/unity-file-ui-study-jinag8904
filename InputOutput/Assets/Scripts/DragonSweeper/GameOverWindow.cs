using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;

    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;

    public TextMeshProUGUI totalScoreValue;

    public Button nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);
    }

    public override void Open()
    {
        base.Open();


    }

    public override void Close()
    {
        base.Close();


    }

    public void OnNext()
    {
        windowManager.Open(0);
    }
}