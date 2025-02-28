using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Scenes/Levels/Level 1"); 
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit(); 
        #endif
    }
    public void OptionsGame()
    {
        SceneManager.LoadSceneAsync("Scenes/Menus/MenuOptions");
    }
}


