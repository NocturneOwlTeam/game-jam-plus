using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore { get; private set; }

    [SerializeField]
    private ScoreSaver scoreSaver;
    [SerializeField]
    private ComboSystem comboSystem;

    [SerializeField]
    private TextMeshProUGUI currentScoreText;

    [SerializeField]
    private RectTransform scorePanel;
    [SerializeField]
    private Canvas canvasScore;

    [Header("Panel de Score final")]
    [SerializeField]
    private TextMeshProUGUI finalScore;

    [SerializeField]
    private TextMeshProUGUI highScore;
    private void Start()
    {
        if (!scoreSaver)
        {
            Debug.LogError("Asigna un ScoreSaver!");
        }
        currentScoreText.text = $"{currentScore}";
        canvasScore.enabled = false;
    }
    public void SetLevel1Score()
    {
        comboSystem.SetHighestCombo();
        int comboTotal = comboSystem.highestComboCount * 10;
        currentScore += comboTotal;
        scoreSaver.score1 = Mathf.Max(scoreSaver.score1, currentScore);
        finalScore.text = $"{currentScore}";
        highScore.text = $"{scoreSaver.score1}";
        canvasScore.enabled = true;
        scorePanel.DOAnchorPosY(0, 0.35f);
    }

    public void SetLevel2Score()
    {
        comboSystem.SetHighestCombo();
        int comboTotal = comboSystem.highestComboCount * 10;
        currentScore += comboTotal;
        scoreSaver.score2 = Mathf.Max(scoreSaver.score2, currentScore);
        finalScore.text = $"{currentScore}";
        highScore.text = $"{scoreSaver.score1}";
        canvasScore.enabled = true;
        scorePanel.DOAnchorPosY(0, 0.35f);
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;
        currentScoreText.text = $"{currentScore}";
    }

    private void OnEnable()
    {
        ScoreIncreaser.OnIncrease += IncreaseScore;
    }

    private void OnDisable()
    {
        ScoreIncreaser.OnIncrease -= IncreaseScore;
    }
}