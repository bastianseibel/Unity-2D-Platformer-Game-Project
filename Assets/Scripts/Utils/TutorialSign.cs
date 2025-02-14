using UnityEngine;
using TMPro;

public class TutorialSign : MonoBehaviour
{
    [Header("Tutorial UI")]
    [SerializeField] private string tutorialMessage;
    [SerializeField] private GameObject tutorialUI;

    [Header("Audio")]
    [SerializeField] private AudioSource tutorialSound;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tutorialUI.TryGetComponent<TextMeshProUGUI>(out var tmpText))
            {
                tmpText.text = tutorialMessage;
                tutorialUI.SetActive(true);
                tutorialSound.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialUI.SetActive(false);
            tutorialSound.Stop();
        }
    }
}