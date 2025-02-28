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

    public void Level1()
    {
        SceneManager.LoadSceneAsync("Scenes/Levels/Level 1"); 
    }

    public void Level2()
    {
        SceneManager.LoadSceneAsync("Scenes/Levels/Level 2"); 
    }

    public void Level3()
    {
        SceneManager.LoadSceneAsync("Scenes/Levels/Level 3"); 
    }

    public void Level4()
    {
        SceneManager.LoadSceneAsync("Scenes/Levels/Level 4"); 
    }

    public void LevelsMenu()
    {
        SceneManager.LoadSceneAsync("Scenes/Menus/LevelsMenu"); 
    }
    
}
