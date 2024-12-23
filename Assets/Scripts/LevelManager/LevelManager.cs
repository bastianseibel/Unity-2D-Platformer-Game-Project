using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private CanvasGroup fadeImage;
    [SerializeField] private float fadeSpeed = 1f;

    private bool isTransitioning = false;

    // * Singleton pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // * Start method to initialize fade image
    private void Start()
    {
        fadeImage.alpha = 0;
        fadeImage.gameObject.SetActive(false);
    }

    // * Load level complete scene
    public void LoadLevelCompleteScene()
    {
        StartCoroutine(LoadLevelWithFade("LevelCompleteScene"));
    }

    // * Load next level
    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = (currentIndex == 3) ? 2 : currentIndex + 1;

        if (SaveLoadManager.Instance.IsLevelUnlocked(nextIndex - 1))
        {
            StartCoroutine(LoadLevelWithFade(nextIndex));
        }
        else
        {
            Debug.Log("Level not unlocked");
        }
    }

    // * Load a specific level
    public void LoadLevel(int levelIndex)
    {
        if (SaveLoadManager.Instance.IsLevelUnlocked(levelIndex))
        {
            StartCoroutine(LoadLevelWithFade(levelIndex));
        }
        else
        {
            Debug.Log("Level not unlocked");
        }
    }

    // * Load main menu
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelWithFade("MainMenu"));
    }

    // * Load level with fade effect
    private IEnumerator LoadLevelWithFade(string sceneName)
    {
        isTransitioning = true;
        yield return FadeIn();
        SceneManager.LoadScene(sceneName);
        yield return FadeOut();
        isTransitioning = false;
    }

    // * Load level with fade effect
    private IEnumerator LoadLevelWithFade(int sceneIndex)
    {
        isTransitioning = true;
        yield return FadeIn();
        SceneManager.LoadScene(sceneIndex);
        yield return FadeOut();
        isTransitioning = false;
    }

    // * Fade in effect
    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        while (fadeImage.alpha < 1)
        {
            fadeImage.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    // * Fade out effect
    private IEnumerator FadeOut()
    {
        while (fadeImage.alpha > 0)
        {
            fadeImage.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }
}