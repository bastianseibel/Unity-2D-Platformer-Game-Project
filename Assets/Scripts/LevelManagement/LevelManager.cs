using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public LevelState CurrentState { get; private set; }
    private LevelTransitionManager transitionManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CurrentState = LevelState.Playing;

            // * Search for the TransitionManager in the scene
            transitionManager = FindObjectOfType<LevelTransitionManager>();

            if (transitionManager == null)
            {
                Debug.LogError("LevelTransitionManager not found!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevelCompleteScene()
    {
        SetLevelState(LevelState.Completed);
        StartCoroutine(LoadLevelWithFade("LevelCompleteScene"));
    }

    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = (currentIndex == 3) ? 2 : currentIndex + 1;

        if (SaveLoadManager.Instance.IsLevelUnlocked(nextIndex - 1))
        {
            LevelEvents.TriggerLevelLoading(nextIndex);
            StartCoroutine(LoadLevelWithFade(nextIndex));
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (SaveLoadManager.Instance.IsLevelUnlocked(levelIndex))
        {
            LevelEvents.TriggerLevelLoading(levelIndex);
            StartCoroutine(LoadLevelWithFade(levelIndex));
        }
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelWithFade("MainMenu"));
    }

    private IEnumerator LoadLevelWithFade(string sceneName)
    {
        SetLevelState(LevelState.Transitioning);
        yield return transitionManager.FadeIn();
        SceneManager.LoadScene(sceneName);
        yield return transitionManager.FadeOut();
        SetLevelState(LevelState.Playing);
    }

    private IEnumerator LoadLevelWithFade(int sceneIndex)
    {
        SetLevelState(LevelState.Transitioning);
        yield return transitionManager.FadeIn();
        SceneManager.LoadScene(sceneIndex);
        yield return transitionManager.FadeOut();
        SetLevelState(LevelState.Playing);
    }

    private void SetLevelState(LevelState newState)
    {
        CurrentState = newState;
        LevelEvents.TriggerLevelStateChanged(newState);
    }
}