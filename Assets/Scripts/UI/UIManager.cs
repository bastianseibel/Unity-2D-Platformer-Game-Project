using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Canvas References")]
    [SerializeField] private Canvas gameplayHUD;
    [SerializeField] private Canvas menuCanvas;

    private bool isMenuActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeUI()
    {
        ShowGameplayHUD();
    }

    public void ShowGameplayHUD()
    {
        if (gameplayHUD != null)
        {
            gameplayHUD.enabled = true;
            isMenuActive = false;
            UIEvents.TriggerMenuClosed();
        }

        if (menuCanvas != null)
        {
            menuCanvas.enabled = false;
        }
    }

    public void ShowMenu()
    {
        if (menuCanvas != null)
        {
            menuCanvas.enabled = true;
            isMenuActive = true;
            UIEvents.TriggerMenuOpened();
        }

        if (gameplayHUD != null)
        {
            gameplayHUD.enabled = false;
        }
    }

    public bool IsMenuActive()
    {
        return isMenuActive;
    }
}