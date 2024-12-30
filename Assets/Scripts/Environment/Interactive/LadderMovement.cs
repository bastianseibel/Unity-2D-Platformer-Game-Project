using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 8f;
    private LadderController controller;
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private bool isOnLadder = false;

    private void Start()
    {
        controller = new LadderController();
        controller.Initialize();
        UIEvents.OnControlButtonStateChanged += HandleControlInput;
    }

    private void OnDestroy()
    {
        UIEvents.OnControlButtonStateChanged -= HandleControlInput;
    }

    private void HandleControlInput(string buttonType, bool isPressed)
    {
        if (!isOnLadder) return;

        switch (buttonType.ToLower())
        {
            case "up":
                isMovingUp = isPressed;
                if (!isPressed)
                {
                    controller.UpdateMovement(0, 0);
                }
                break;
            case "down":
                isMovingDown = isPressed;
                if (!isPressed)
                {
                    controller.UpdateMovement(0, 0);
                }
                break;
        }
    }

    private void Update()
    {
        if (isOnLadder)
        {
            float direction = 0;
            if (isMovingUp) direction = 1;
            if (isMovingDown) direction = -1;

            controller.UpdateMovement(direction, climbSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnLadder = true;
            controller.StartClimbing();
            Debug.Log("Player entered ladder");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnLadder = false;
            controller.StopClimbing();
            isMovingUp = false;
            isMovingDown = false;
            Debug.Log("Player exited ladder");
        }
    }
}