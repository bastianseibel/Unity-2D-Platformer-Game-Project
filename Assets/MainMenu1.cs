using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Scenes/Levels/Level 1"); // Achte auf den korrekten Namen!
    }
}

