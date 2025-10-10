using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RandomTetromino spawner;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI levelText;
    public bool isGameOver = false;
    public float fallTime = 0.8f;
    public Button startButton;

    [Header("Gameplay")]
    private int lastScore = 0;
    private int score = 0;
    private int level = 0;
    private float elapsedTime = 0f;
    private int highScore = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
        UpdateTimeUI();

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeUI();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score - lastScore >= 500)
        {
            lastScore = score;
            level += 1;
            if (fallTime > 0.1f)
                fallTime -= 0.1f;
        }
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (highScoreText != null)
            highScoreText.text = "Top: " + highScore;

        if (levelText != null)
            levelText.text = "Level: " + level;
    }

    void UpdateTimeUI()
    {
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            timeText.text = $"Time: {minutes}:{seconds:00}";
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.text = $"Game Over\nScore: {score}\nHigh Score: {highScore}";
    }

    public float upLevel()
    {
        return fallTime;
    }

    public void OnStartButtonClick()
    {
        spawner.enabled = true;
        startButton.gameObject.SetActive(false);
    }
}

