using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string buttonType;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (UIManager.Instance != null && UIManager.Instance.IsMenuActive())
            return;

        switch(buttonType.ToLower())
        {
            case "left":
            case "right":
            case "up":
            case "down":
                UIEvents.TriggerControlButtonState(buttonType, true);
                break;
            case "jump":
                UIEvents.TriggerControlButtonState("jump", true);
                break;
            case "attack":
                UIEvents.TriggerAttackButton();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (UIManager.Instance != null && UIManager.Instance.IsMenuActive())
            return;

        switch(buttonType.ToLower())
        {
            case "left":
            case "right":
            case "up":
            case "down":
                UIEvents.TriggerControlButtonState(buttonType, false);
                break;
            case "jump":
                UIEvents.TriggerControlButtonState("jump", false);
                break;
        }
    }
}