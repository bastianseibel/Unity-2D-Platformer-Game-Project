using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [Header("Heart Sprites")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void OnEnable()
    {
        UIEvents.OnHealthChanged += UpdateHearts;
    }

    private void OnDisable()
    {
        UIEvents.OnHealthChanged -= UpdateHearts;
    }

    private void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}