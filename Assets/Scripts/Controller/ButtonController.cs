using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // * References to other Player Components
    private HeroMovementController movementController;
    private Rigidbody2D playerRb;
    private HeroAttack heroAttack;
    private LadderMovement ladderMovement;

    // * Type of the button
    public string buttonType;

    // * Get all necessary components from the player
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            movementController = player.GetComponent<HeroMovementController>();
            playerRb = player.GetComponent<Rigidbody2D>();
            heroAttack = player.GetComponent<HeroAttack>();
        }
        ladderMovement = FindObjectOfType<LadderMovement>();
    }

    // * Handle the button press events
    public void OnPointerDown(PointerEventData eventData)
    {
        switch(buttonType.ToLower())
        {
            case "left":
                movementController.Move(-1);
                break;
            case "right":
                movementController.Move(1);
                break;
            case "jump":
                movementController.Jump();
                break;
            case "up":
                if (ladderMovement != null)
                    ladderMovement.OnUpButtonDown();
                break;
            case "down":
                if (ladderMovement != null)
                    ladderMovement.OnDownButtonDown();
                break;
            case "attack":
                if (heroAttack != null)
                {
                    heroAttack.OnAttackButtonPressed();
                }
                break;
        }
    }

    // * Handle the button release events
    public void OnPointerUp(PointerEventData eventData)
    {
        switch(buttonType.ToLower())
        {
            case "left":
            case "right":
                movementController.Move(0);
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