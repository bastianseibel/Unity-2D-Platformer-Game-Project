using UnityEngine;
using TMPro;

public class TutorialSign : MonoBehaviour
{
    // * Tutorial UI elements
    [SerializeField] private string tutorialMessage;
    [SerializeField] private GameObject tutorialUI;
    
    // * Show tutorial message when player enters trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tutorialUI.TryGetComponent<TextMeshProUGUI>(out var tmpText))
            {
                tmpText.text = tutorialMessage;
                tutorialUI.SetActive(true);
            }
        }
    }

    // * Hide tutorial message when player leaves trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialUI.SetActive(false);
        }
    }
}