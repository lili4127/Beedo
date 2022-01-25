using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    public float difficulty;
    public bool easyDifficultyOn;

    private void Awake()
    {
        scoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString();
        difficulty = PlayerPrefs.GetFloat("difficulty", 3f);
        easyDifficultyOn = difficulty == 3;
        difficultyText.text = easyDifficultyOn ? "Easy" : "Hard";
    }

    public void ResetHighscore()
    {
        scoreText.text = "High Score: " + "0";
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
    }

    public void SetDifficulty()
    {
        easyDifficultyOn = !easyDifficultyOn;
        difficulty = easyDifficultyOn ? 3f : 2f;
        PlayerPrefs.SetFloat("difficulty", difficulty);
        PlayerPrefs.Save();
        difficultyText.text = easyDifficultyOn ? "Easy" : "Hard";
    }
}
