using UnityEngine;
using System.Collections;

public class LevelTransitionManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeImage;
    [SerializeField] private float fadeSpeed = 1f;

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.alpha = 0;
            fadeImage.gameObject.SetActive(false);
        }
    }

    public IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        while (fadeImage.alpha < 1)
        {
            fadeImage.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        while (fadeImage.alpha > 0)
        {
            fadeImage.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }
}