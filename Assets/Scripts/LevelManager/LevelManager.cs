using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private CanvasGroup fadeImage;
    [SerializeField] private float fadeSpeed = 1f;
    
    private bool isTransitioning = false;

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

    private void Start()
    {
        fadeImage.alpha = 0;
        fadeImage.gameObject.SetActive(false);
    }

    public void LoadLevelCompleteScene()
    {
        StartCoroutine(LoadLevelWithFade("LevelCompleteScene"));
    }

    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        
    if (currentIndex == 3)
        {
            StartCoroutine(LoadLevelWithFade(2));
        }
        else
        {
            StartCoroutine(LoadLevelWithFade(currentIndex + 1));
        }
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelWithFade("MainMenu"));
    }

    private IEnumerator LoadLevelWithFade(string sceneName)
    {
        isTransitioning = true;
        yield return FadeIn();
        SceneManager.LoadScene(sceneName);
        yield return FadeOut();
        isTransitioning = false;
    }

    private IEnumerator LoadLevelWithFade(int sceneIndex)
    {
        isTransitioning = true;
        yield return FadeIn();
        SceneManager.LoadScene(sceneIndex);
        yield return FadeOut();
        isTransitioning = false;
    }

    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        while (fadeImage.alpha < 1)
        {
            fadeImage.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

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