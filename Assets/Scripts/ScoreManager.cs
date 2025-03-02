using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;
    void Start()
    {
        DisplayScore();
    }
    public void IncrementScore(){
        score++;
        DisplayScore();
    }

    public void DisplayScore(){
        scoreText.text = $"Score: {score}";
    }
}
