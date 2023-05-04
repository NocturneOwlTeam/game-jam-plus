using TMPro;
using UnityEngine;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField]
    private ScoreSaver scoreSaver;

    [SerializeField]
    private TextMeshProUGUI scoreTextLevel1;

    [SerializeField]
    private TextMeshProUGUI scoreTextLevel2;

    private void Start()
    {
        if (!scoreSaver)
        {
            Debug.LogError("Asigna un ScoreSaver!");
        }

        scoreTextLevel1.text = $"{scoreSaver.score1}";
        scoreTextLevel2.text = $"{scoreSaver.score2}";
    }
}