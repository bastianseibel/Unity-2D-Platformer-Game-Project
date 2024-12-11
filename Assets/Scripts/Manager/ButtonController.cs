using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private HeroMovement heroMovement;
    private Rigidbody2D playerRb;
    private HeroAttack heroAttack;
    private LadderMovement ladderMovement;
    public string buttonType;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            heroMovement = player.GetComponent<HeroMovement>();
            playerRb = player.GetComponent<Rigidbody2D>();
            heroAttack = player.GetComponent<HeroAttack>();
        }
        ladderMovement = FindObjectOfType<LadderMovement>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch(buttonType.ToLower())
        {
            case "left":
                heroMovement.Move(-1);
                break;
            case "right":
                heroMovement.Move(1);
                break;
            case "up":
                if (ladderMovement != null)
                    ladderMovement.OnUpButtonDown();;
                break;
            case "down":
                if (ladderMovement != null)
                    ladderMovement.OnDownButtonDown();
                break;
            case "jump":
                heroMovement.Jump();
                break;
            case "attack":
                if (heroAttack != null)
                {
                    heroAttack.OnAttackButtonPressed();
                }
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch(buttonType.ToLower())
        {
            case "left":
            case "right":
                heroMovement.Move(0);
                break;
            case "up":
                if (ladderMovement != null)
                    ladderMovement.OnUpButtonUp();
                break;
            case "down":
                if (ladderMovement != null)
                    ladderMovement.OnDownButtonUp();
                break;
        }
    }
}