using UnityEngine;

public class UniqueID : MonoBehaviour
{
    public string uniqueID;

    // * Generates a unique ID for each object
    void Awake()
    {
        if (string.IsNullOrEmpty(uniqueID))
        {
            uniqueID = System.Guid.NewGuid().ToString();
        }
    }
}