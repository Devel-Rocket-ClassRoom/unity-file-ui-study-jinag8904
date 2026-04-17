using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverWindow : GenericWindow
{
    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;
    
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;

    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;

    public TextMeshProUGUI totalScoreValue;

    public Button nextButton;

    private float revealDelay = 1f;
    private float revealDuration = 5f;
    private Coroutine coroutine;

    public int statsCount = 3;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);

        statsLabels = new TextMeshProUGUI[] {leftStatLabel, rightStatLabel};
        statsValues = new TextMeshProUGUI[] {leftStatValue, rightStatValue};
    }

    public override void Open()
    {
        leftStatLabel.text = leftStatValue.text = rightStatLabel.text = rightStatValue.text = string.Empty;
        totalScoreValue.text = $"{0:D8}";

        var leftScores = new int[statsCount];
        var rightScores = new int[statsCount];

        for (int i = 0; i < statsCount; i++)
        {
            leftScores[i] = Random.Range(0, 10000);
            rightScores[i] = Random.Range(0, 10000);
        }

        var totalScore = Random.Range(0, 100000000);
        
        base.Open();

        coroutine = StartCoroutine(Co_Reveal(leftScores, rightScores, totalScore));
    }

    IEnumerator Co_Reveal(int[] leftScores, int[] rightScores, int totalScore)
    {
        for (int i = 0; i < statsCount; i++)
        {
            yield return new WaitForSeconds(revealDelay);

            string newLine = (i == 0) ? string.Empty : "\n";

            leftStatLabel.text = $"{leftStatLabel.text}{newLine}Stat {i}";
            leftStatValue.text = $"{leftStatValue.text}{newLine}{leftScores[i]:D4}";
        }

        for (int i = 0; i < statsCount; i++)
        {
            yield return new WaitForSeconds(revealDelay);

            string newLine = (i == 0) ? string.Empty : "\n";

            rightStatLabel.text = $"{rightStatLabel.text}{newLine}Stat {i}";
            rightStatValue.text = $"{rightStatValue.text}{newLine}{rightScores[i]:D4}";
        }

        yield return new WaitForSeconds(revealDelay);

        var t = 0f;
        var elapsed = 0f;

        while (t < 1f)
        {
            elapsed += Time.deltaTime;
            t = elapsed / revealDuration;
            var currentScore = Mathf.FloorToInt(Mathf.Lerp(0, totalScore, t));

            totalScoreValue.text = $"{currentScore:D8}";
            yield return null;
        }

        totalScoreValue.text = $"{totalScore:D8}";
    }

    public override void Close()
    {
        base.Close();
        StopCoroutine(coroutine);
    }

    public void OnNext()
    {
        windowManager.Open(2);
    }
}