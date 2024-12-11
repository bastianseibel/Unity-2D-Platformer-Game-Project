using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private HeroMovement heroMovement;
    public string buttonType;
    private Rigidbody2D playerRb;
    private HeroAttack heroAttack;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        heroMovement = player.GetComponent<HeroMovement>();
        playerRb = player.GetComponent<Rigidbody2D>();
        heroAttack = player.GetComponent<HeroAttack>();
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
                if(heroMovement.isOnLadder)
                    playerRb.velocity = new Vector2(playerRb.velocity.x, heroMovement.speed);
                break;
            case "down":
                if(heroMovement.isOnLadder)
                    playerRb.velocity = new Vector2(playerRb.velocity.x, -heroMovement.speed);
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
            case "down":
                if(heroMovement.isOnLadder)
                    playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                break;
        }
    }
}