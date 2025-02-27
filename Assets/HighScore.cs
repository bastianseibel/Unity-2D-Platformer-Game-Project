using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HigScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        highscoreText.text = "";
        for (int i = 0; i < 4; i++)
        {
            int highscore = PlayerPrefs.GetInt($"Highscore{i}", 0);
            highscoreText.text += $"Level {i + 1}: {highscore}\n";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnVolumeChange(float volume) {
        AudioListener.volume = volume;
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Scenes/Menus/Menu");
    }
}
