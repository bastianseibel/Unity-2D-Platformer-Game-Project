using System.Collections.Generic; 
using UnityEngine;

[ExecuteInEditMode]
public class BackgroundParallax : MonoBehaviour
{
    public ParallaxCamera parallaxCamera; 
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>(); 

    
    void Start()
    {
        // * Get the ParallaxCamera reference if not already assigned
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        // * Subscribe to the camera's movement event
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        // * Populate the parallax layers list
        SetLayers();
    }

    // * Sets all child objects with a ParallaxLayer component into the list
    void SetLayers()
    {
        parallaxLayers.Clear(); 

        // * Iterate through all child objects
        for (int i = 0; i < transform.childCount; i++)
        {
            // * Check if the child has a ParallaxLayer component
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            // * Add the layer to the list if found
            if (layer != null)
            {
                layer.name = "Layer-" + i; // * Rename the layer for organization
                parallaxLayers.Add(layer);
            }
        }
    }

    // * Moves each parallax layer based on the camera's movement
    void Move(float delta)
    {
        // * Apply movement to all layers in the list
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}
