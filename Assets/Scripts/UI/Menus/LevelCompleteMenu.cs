using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteMenu : MonoBehaviour
{
    private Button nextLevelButton;
    private Button mainMenuButton;

    private void OnEnable()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            nextLevelButton = canvas.transform.Find("NextLevelButton")?.GetComponent<Button>();
            mainMenuButton = canvas.transform.Find("MainMenuButton")?.GetComponent<Button>();

            if (nextLevelButton != null && mainMenuButton != null)
            {
                nextLevelButton.onClick.AddListener(OnNextLevelButton);
                mainMenuButton.onClick.AddListener(OnMainMenuButton);
            }
        }
    }

    public void OnNextLevelButton()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }

    public void OnMainMenuButton()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadMainMenu();
        }
    }
}