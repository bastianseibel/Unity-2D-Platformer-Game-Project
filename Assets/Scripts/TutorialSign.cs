using UnityEngine;
using UnityEngine.UI;

public class TutorialSign : MonoBehaviour
{
    public string tutorialMessage;
    public GameObject tutorialUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialUI.GetComponent<Text>().text = tutorialMessage;
            tutorialUI.SetActive(true);
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