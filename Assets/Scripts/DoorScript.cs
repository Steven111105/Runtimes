using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public static DoorScript instance; // Singleton instance for easy access
    public bool hasKey = false; // Flag to indicate if the key has been taken

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            hasKey = false; // Initialize hasKey to false
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenDoor()
    {
        if (hasKey)
        {
            Debug.Log("Door opened!");
            Debug.Log("Runtimes Ending");
            EndScreenScript.instance.ShowEndScreen(5); // Show the end screen if the door is opened
            // Add logic to open the door, e.g., play animation, change state, etc.
        }
        else
        {
            Debug.Log("You need a key to open this door.");
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null; // Clear the instance if this is the one being destroyed
        }
    }
}
