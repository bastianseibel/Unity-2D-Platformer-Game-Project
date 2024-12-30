using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Für das Wechseln der Szenen erforderlich

public class Startbutton : MonoBehaviour
{
    // Diese Methode wird beim Klick auf den Button aufgerufen
    public void StartGame()
    {
        // Lade die Szene mit dem Namen "TutorialScene"
        SceneManager.LoadScene("TutorialScene");
    }
}

