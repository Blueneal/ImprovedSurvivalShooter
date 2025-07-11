using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static int score;
    public static int highScore;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textHighScore;

    private void Awake()
    {
        Instance = this;
        score = 0;
        LoadGameState();
        textHighScore.text = "High Score: " + highScore.ToString();
    }

    void Update()
    {
        text.text = "Score: " + score;
    }

    public void SaveGameState()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void LoadGameState()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            textHighScore.text = "High Score: " + highScore;
        }
        else
        {
            highScore = 0;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void ShowScore()
    {
        text.text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
            textHighScore.text = "High Score: " + highScore;
            SaveGameState();
        }
    }
}
