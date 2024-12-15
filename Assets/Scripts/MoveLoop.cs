using UnityEngine;

public class MoveLoop : MonoBehaviour
{
    // * Movement settings
    public GameObject targetObject;
    public float stepHeight = 0.5f;
    public float stepDuration = 0.2f;

    // * Track position and movement state
    private float originalY;
    private int currentStep = 0;
    private float stepTimer = 0f;

    // * Store initial Y position
    void Start()
    {
        originalY = targetObject.transform.localPosition.y;
    }

    // * Update movement timer and trigger next step
    void Update()
    {
        stepTimer += Time.deltaTime;

        if (stepTimer >= stepDuration)
        {
            stepTimer = 0f;
            MoveToNextStep();
        }
    }

    // * Move object up or down in a loop pattern
    private void MoveToNextStep()
    {
        float direction = (currentStep < 2) ? stepHeight : -stepHeight;
        targetObject.transform.localPosition += new Vector3(0, direction, 0);
        
        currentStep++;
        
        if (currentStep >= 4)
        {
            currentStep = 0;
            targetObject.transform.localPosition = new Vector3(
                targetObject.transform.localPosition.x, 
                originalY, 
                targetObject.transform.localPosition.z
            );
        }
    }
}