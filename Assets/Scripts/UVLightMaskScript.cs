using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVLightMaskScript : MonoBehaviour
{
    [Header("Flashlight Settings")]
    public bool hasFlashlightUnlocked = false;
    
    [Header("Position Settings")]
    public Vector3 lockedPosition = new Vector3(-5f, -50f, 0f); // Position when flashlight is locked
    
    void Start()
    {
        UpdateMaskPosition();
    }

    void Update()
    {
        UpdateMaskPosition();
    }
    
    void UpdateMaskPosition()
    {
        if (hasFlashlightUnlocked)
        {
            // Follow cursor when unlocked
            FollowCursor();
        }
        else
        {
            // Move to locked position when locked
            transform.position = lockedPosition;
        }
    }
    
    void FollowCursor()
    {
        if (Camera.main != null)
        {
            // Convert mouse position to world position
            Vector3 mousePosition = Input.mousePosition;
            
            // Set the Z distance from camera (use the current mask's Z position relative to camera)
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            transform.position = worldPosition;
        }
    }
    
    // Public method to unlock the flashlight
    public void UnlockFlashlight()
    {
        hasFlashlightUnlocked = true;
    }
    
    // Public method to lock the flashlight
    public void LockFlashlight()
    {
        hasFlashlightUnlocked = false;
    }
    
    // Public method to toggle flashlight state
    public void ToggleFlashlight()
    {
        hasFlashlightUnlocked = !hasFlashlightUnlocked;
    }
}
