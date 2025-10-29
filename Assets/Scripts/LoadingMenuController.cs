using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingMenuController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject lightEffect;
    public GameObject logo;
    public Slider loadingSlider;
    public Button playButton;

    [Header("Settings")]
    public float fakeLoadingTime = 2f;

    private float timer = 0f;
    private bool isLoading = true;

    void Start()
    {
        loadingSlider.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);

        loadingSlider.value = 0;
    }

    void Update()
    {
        if (isLoading)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fakeLoadingTime);
            loadingSlider.value = progress;

            if (progress >= 1f)
            {
                isLoading = false;
                Invoke(nameof(ShowPlayButton), 0.3f);
            }
        }
    }

    void ShowPlayButton()
    {
        loadingSlider.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}
