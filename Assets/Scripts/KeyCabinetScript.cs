using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCabinetScript : MonoBehaviour
{
    public static KeyCabinetScript instance; // Singleton instance for easy access
    public GameObject key; // Reference to the key cabinet UI
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false); // Ensure the key cabinet is not active at the start
        key.SetActive(false); // Hide the key at the start
    }

    public void OpenKeyCabinet()
    {
        gameObject.SetActive(true); // Show the key cabinet UI
        key.SetActive(true); // Show the key
    }

    public void GetKey()
    {
        key.SetActive(false); // Hide the key after it has been taken
        DoorScript.instance.hasKey = true; // Set the door script to indicate the key has been taken
    }
    
    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null; // Clear the instance if this is the one being destroyed
        }
    }
}
