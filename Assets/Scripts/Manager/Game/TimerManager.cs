using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;
    private float currentTime;
    private bool isTimerRunning;

    void Awake()
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
    void Start() => StartTimer();

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timerText.text = $"Zeit: {time.Minutes:00}:{time.Seconds:00}:{time.Milliseconds:000}";
        }
    }

    public void StartTimer()
    {
        currentTime = 0f;
        isTimerRunning = true;
    }
    public void StopTimer()
    {
        if (!isTimerRunning) return;
        
        isTimerRunning = false;
        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.SaveLevelTime(SceneManager.GetActiveScene().buildIndex, currentTime);
        }
    }

    public float GetCurrentTime() => currentTime;
}