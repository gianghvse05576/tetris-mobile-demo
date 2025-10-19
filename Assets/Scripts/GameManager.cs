using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI levelText;
    public Button startButton;

    [Header("Gameplay")]
    public RandomTetromino spawner;
    public bool isGameOver = false;
    private int score = 0;
    private int level = 0;
    private int highScore = 0;
    private int lastScore = 0;
    public float fallTime = 0.8f;
    private float elapsedTime = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        GameEvents.OnScoreAdded += AddScore;
        GameEvents.OnLineCleared += HandleLineCleared;
        GameEvents.OnGameOver += HandleGameOver;
    }

    void OnDisable()
    {
        GameEvents.OnScoreAdded -= AddScore;
        GameEvents.OnLineCleared -= HandleLineCleared;
        GameEvents.OnGameOver -= HandleGameOver;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
        UpdateTimeUI();
    }

    void Update()
    {
        if (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeUI();
        }
    }

    void AddScore(int amount)
    {
        score += amount;
        if (score - lastScore >= 500)
        {
            lastScore = score;
            level++;
            if (fallTime > 0.1f)
                fallTime -= 0.1f;
            GameEvents.RaiseLevelUp(level);
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateScoreUI();
    }

    void HandleLineCleared(int lines)
    {
        int scoreToAdd = lines * 100;
        GameEvents.RaiseScoreAdded(scoreToAdd);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
        if (highScoreText != null)
            highScoreText.text = $"Top: {highScore}";
        if (levelText != null)
            levelText.text = $"Level: {level}";
    }

    void UpdateTimeUI()
    {
        if (timeText == null) return;
        int m = Mathf.FloorToInt(elapsedTime / 60);
        int s = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"Time: {m}:{s:00}";
    }

    void HandleGameOver()
    {
        isGameOver = true;
        if (gameOverText != null)
            gameOverText.text = $"Game Over\nScore: {score}\nHigh Score: {highScore}";
    }

    public float GetFallTime() => fallTime;

    public void OnStartButtonClick()
    {
        startButton.gameObject.SetActive(false);
        spawner.enabled = true;
    }
}
