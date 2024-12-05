using UnityEngine;

public class MoveLoop : MonoBehaviour
{
    public GameObject targetObject;
    public float stepHeight = 0.5f;
    public float stepDuration = 0.2f;

    private float originalY;
    private int currentStep = 0;
    private float stepTimer = 0f;

    void Start()
    {
        originalY = targetObject.transform.localPosition.y;
    }

    void Update()
    {
        stepTimer += Time.deltaTime;

        if (stepTimer >= stepDuration)
        {
            stepTimer = 0f;
            MoveToNextStep();
        }
    }

    private void MoveToNextStep()
    {
        float direction = (currentStep < 2) ? stepHeight : -stepHeight;
        targetObject.transform.localPosition += new Vector3(0, direction, 0);
        
        currentStep++;
        
        if (currentStep >= 4)
        {
            currentStep = 0;
            targetObject.transform.localPosition = new Vector3(targetObject.transform.localPosition.x, originalY, targetObject.transform.localPosition.z); // Zurück zur ursprünglichen Position
        }
    }
}