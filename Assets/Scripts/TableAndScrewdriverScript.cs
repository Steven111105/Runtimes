using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableAndScrewdriverScript : MonoBehaviour
{
    public static TableAndScrewdriverScript instance; // Singleton instance for easy access
    public bool hasScrewdriver = false; // Flag to indicate if the screwdriver has been taken
    public GameObject screwdriver; // Reference to the screwdriver GameObject
    public GameObject drawer; // Reference to the drawer GameObject
    public Sprite openDrawerSprite; // Sprite to show when the drawer is open

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        screwdriver.SetActive(false);
        drawer.SetActive(true);
        hasScrewdriver = false; // Initialize hasScrewdriver to false
    }

    public void DrawerClicked()
    {
        drawer.SetActive(false); // Hide the drawer when clicked
        SoundInstance.Instance.PlayDrawer(); // Play the drawer opening sound
        screwdriver.SetActive(true);
        GetComponent<Image>().sprite = openDrawerSprite; // Change the sprite to the open drawer sprite
    }

    public void TakeScrewdriver()
    {
        hasScrewdriver = true; // Set the flag to indicate the screwdriver has been taken
        SoundInstance.Instance.PlayTakeItem(); // Play the sound for taking an item
        screwdriver.SetActive(false); // Hide the screwdriver GameObject
        Debug.Log("Screwdriver taken!");
        // Optionally, you can add logic to update the UI or notify other scripts
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
