using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiNavigation : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Scenes/Levels/Level 1"); 
    }

    public void OptionsGame()
    {
        SceneManager.LoadSceneAsync("Scenes/Menus/MenuOptions");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit(); 
        #endif
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Scenes/Menus/Menu");
    }
    
}
