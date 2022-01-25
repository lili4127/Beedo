using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private BallPool ballPool;
    private bool isPaused = false;
    private float difficulty;

    [Space]
    [Header("Backgrounds")]
    [SerializeField] private int playerStrikes;
    [SerializeField] private int activeBackground;
    [SerializeField] private SpriteRenderer[] backgrounds;
    private Color targetColor = new Color(1, 1, 1, 0);

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    public bool timerGoing { get; private set; }
    private float score = 0f;
    private float pointsPerSecond = 10;

    [Header("Tutorial")]
    [SerializeField] private GameObject tutorialPanel;
    public bool tutorial { get; private set; }
    [SerializeField] private int tutorialState;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("tutorial", 0) == 1)
        {
            tutorial = true;
        }

        difficulty = PlayerPrefs.GetFloat("difficulty", 3f);
    }

    private void OnEnable()
    {
        Ball.ballLost += UpdateStrikes;
        Ball.ballSaved += UpdateScore;
    }

    private void Start()
    {
        if (!tutorial)
        {
            SetUpGame();
            StartGame();
        }

        else
        {
            StartTutorial();
            PlayerPrefs.SetInt("tutorial", 0);
            PlayerPrefs.Save();
        }
    }

    private void SetUpGame()
    {
        //set background opacities to 1
        foreach (SpriteRenderer i in backgrounds)
        {
            i.color = Color.white;
        }
        CleanBallsOffScreen();
        activeBackground = 0;
        playerStrikes = 0;
        timerGoing = false;
        score = 0f;
        scoreText.text = score.ToString();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        int countdownTime = 3;
        countdownText.text = countdownTime.ToString();
        countdownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        HUD.SetActive(true);
        StartTimer();
        StartCoroutine(ServeBalls());
    }

    private void StartTimer()
    {
        timerGoing = true;
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            score += pointsPerSecond * Time.deltaTime;
            scoreText.text = Mathf.FloorToInt(score).ToString();
            yield return null;
        }
    }

    private void StopTimer()
    {
        timerGoing = false;
    }

    IEnumerator ServeBalls()
    {
        while (timerGoing)
        {
            Ball b = ballPool.Get();
            b.gameObject.SetActive(true);
            b.Serve();
            
            yield return new WaitForSeconds(difficulty);
        }
    }

    private void UpdateScore()
    {
        if (timerGoing)
        {
            StartCoroutine(PulseTextCo());
        }
    }

    IEnumerator PulseTextCo()
    {
        for (float i = 1f; i <= 1.2f; i += 0.05f)
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }

        scoreText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        scoreText.color = Color.green;
        score += 50;
        scoreText.text = score.ToString();

        for (float i = 1.2f; i >= 1f; i -= 0.05f)
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }

        scoreText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        scoreText.color = Color.white;
    }

    private void UpdateStrikes()
    {
        if (timerGoing)
        {
            playerStrikes++;

            if (playerStrikes % 5 == 0 && activeBackground <= 3)
            {
                if (activeBackground == 3)
                {
                    LoseGame();
                }

                else
                {
                    StartCoroutine(BackgroundCo(backgrounds[activeBackground], targetColor, 5));
                    activeBackground++;
                }
            }
        }
    }

    IEnumerator BackgroundCo(SpriteRenderer imageToFade, Color endValue, float duration)
    {
        float time = 0;
        Color startValue = imageToFade.color;

        while (time < duration)
        {
            imageToFade.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        imageToFade.color = endValue;
    }

    private void LoseGame()
    {
        HUD.SetActive(false);
        CleanBallsOffScreen();
        StopTimer();
        SetHighScore();
        gameOverPanel.SetActive(true);
    }

    private void SetHighScore()
    {
        if (PlayerPrefs.GetInt("highscore", 0) < Mathf.FloorToInt(score))
        {
            finalScoreText.text = "New High Score!";
            highScoreText.text = scoreText.text;
            PlayerPrefs.SetInt("highscore", Mathf.FloorToInt(score));
            PlayerPrefs.Save();
        }

        else
        {
            finalScoreText.text = "Final Score: " + scoreText.text;
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString();
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        HUD.SetActive(!isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
            StopTimer();
            pausePanel.SetActive(true);
        }

        else
        {
            Time.timeScale = 1f;
            StartTimer();
            pausePanel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        StopAllCoroutines();
        SetUpGame();
        StartGame();
    }

    private void CleanBallsOffScreen()
    {
        foreach (Transform t in ballPool.transform)
        {
            t.GetComponent<Ball>().ResetBall();
        }
    }

    private void StartTutorial()
    {
        tutorialPanel.SetActive(true);
        tutorialState = 0;
        tutorialPanel.transform.GetChild(tutorialState).gameObject.SetActive(true);
    }

    public void TutorialStep(int i)
    {
        tutorialPanel.transform.GetChild(tutorialState).gameObject.SetActive(false);
        tutorialState += i;

        if (tutorialState <= 0)
        {
            tutorialState = 0;
        }

        if (tutorialState >= 6)
        {
            tutorialState = 6;
        }

        tutorialPanel.transform.GetChild(tutorialState).gameObject.SetActive(true);
    }

    public void PlayGame()
    {
        HUD.SetActive(false);
        tutorialPanel.SetActive(false);
        RestartGame();
        tutorial = false;
    }

    private void OnDisable()
    {
        Ball.ballLost -= UpdateStrikes;
        Ball.ballSaved -= UpdateScore;
    }
}
