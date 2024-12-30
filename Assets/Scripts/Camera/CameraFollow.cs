using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // * Reference to the main camera
    private Camera mainCamera;

    // * Get the main camera reference at start
    void Start()
    {
        mainCamera = Camera.main;
    }

    // * Update camera position every frame
    void Update()
    {
        UpdateCamera();
    }

    // * Make camera follow the target by keeping its z-position
    private void UpdateCamera()
    {
        if (mainCamera != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = mainCamera.transform.position.z;
            mainCamera.transform.position = newPosition;
        }
    }
}