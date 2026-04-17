using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;

    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;

    public TextMeshProUGUI totalScoreValue;

    public Button nextButton;

    private float revealDuration = 5f;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);
    }

    public override void Open()
    {
        base.Open();
        leftStatLabel.text = "Stat 1\nStat 2\nStat 3";
        rightStatLabel.text = "Stat 1\nStat 2\nStat 3";

        // 스탯 차례로 출력 (왼쪽 위부터)
        System.Random rand = new();

        var leftLabels = leftStatLabel.text.Split("\n");
        int[] leftScores = new int[] { rand.Next(0, 10000), rand.Next(0, 10000), rand.Next(0, 10000) };
        var rightLabels = rightStatLabel.text.Split("\n");
        int[] rightScores = new int[] { rand.Next(0, 10000), rand.Next(0, 10000), rand.Next(0, 10000) };
        var totalScore = rand.Next(0, 100000000);

        leftStatLabel.text = leftStatValue.text = rightStatLabel.text = rightStatValue.text = string.Empty;
        totalScoreValue.text = $"00000000";

        StartCoroutine(Co_RevealStats(leftLabels, leftScores, rightLabels, rightScores, totalScore));
    }

    IEnumerator Co_RevealStats(string[] leftLabels, int[] leftScores, string[] rightLabels, int[] rightScores, int totalScore)
    {
        for (int i = 0; i < leftLabels.Length; i++)
        {
            yield return new WaitForSeconds(1);
            leftStatLabel.text += leftLabels[i];
            leftStatValue.text += $"{leftScores[i]:0000}";

            if (i != leftLabels.Length)
            {
                leftStatLabel.text += "\n";
                leftStatValue.text += "\n";
            }
        }

        for (int i = 0; i < rightLabels.Length; i++)
        {
            yield return new WaitForSeconds(1);
            rightStatLabel.text += rightLabels[i];
            rightStatValue.text += $"{rightScores[i]:0000}";

            if (i != leftLabels.Length)
            {
                rightStatLabel.text += "\n";
                rightStatValue.text += "\n";
            }
        }

        StartCoroutine(Co_RevealTotalScore(totalScore));
    }

    IEnumerator Co_RevealTotalScore(int totalScore)
    {
        Debug.Log(totalScore);

        yield return new WaitForSeconds(1);

        var currentScore = 0f;
        var elapsedTime = 0f;

        while (currentScore < totalScore)    // 현재 점수가 총점과 같을 때까지 (다른 동안)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / revealDuration;

            currentScore = Mathf.Lerp(currentScore, totalScore, t);
            
            totalScoreValue.text = $"{currentScore:00000000}";
            yield return null;
        }

        totalScoreValue.text = $"{totalScore:00000000}";
        Debug.Log("코루틴 끝");
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