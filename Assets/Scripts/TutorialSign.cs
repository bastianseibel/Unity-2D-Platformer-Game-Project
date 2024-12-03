using UnityEngine;
using TMPro;

public class TutorialSign : MonoBehaviour
{
    [SerializeField] private string tutorialMessage;
    [SerializeField] private GameObject tutorialUI;
    
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialUI.SetActive(false);
        }
    }
}