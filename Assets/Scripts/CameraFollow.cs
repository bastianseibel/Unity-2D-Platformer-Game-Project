using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        UpdateCamera();
    }

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