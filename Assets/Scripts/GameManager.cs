using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    [Header("Gameplay")]
    private int score = 0;
    private float elapsedTime = 0f; // thời gian tích lũy (tính theo giây)

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
        UpdateTimeUI();
    }

    void Update()
    {
        // Cập nhật thời gian mỗi frame
        elapsedTime += Time.deltaTime;
        UpdateTimeUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void UpdateTimeUI()
    {
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            // hiển thị dạng "M:SS" (vd: 0:07, 1:23, 2:05)
            timeText.text = $"Time: {minutes}:{seconds:00}";
        }
    }
}
