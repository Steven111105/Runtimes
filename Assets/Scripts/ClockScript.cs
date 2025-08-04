using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour
{
    public GameObject[] screws;
    public bool[] isUnscrewed;
    public GameObject droppedClock;

    public GameObject uvFlashLight; // Reference to the UV light GameObject

    void Start()
    {
        isUnscrewed = new bool[4];
        droppedClock.SetActive(false); // Ensure the dropped clock is not active at the start
        for (int i = 0; i < screws.Length; i++)
        {

            isUnscrewed[i] = false; // Initialize all screws as not unscrewed
        }
    }

    public void Unscrew(int index)
    {
        if (TableAndScrewdriverScript.instance.hasScrewdriver)
        {
            screws[index].SetActive(false); // Hide the screw GameObject
            isUnscrewed[index] = true;
            SoundInstance.Instance.PlayUnscrew(); // Play the unscrewing sound
        }

        if (AllScrewsUnscrewed())
        {
            droppedClock.SetActive(true); // Show the dropped clock GameObject
            GetComponent<Image>().color = new Color(1, 1, 1, 0f); // Change the clock's color to indicate it's unscrewed
            GetComponent<Image>().raycastTarget = false; // Disable raycasting on the clock
        }
    }

    private bool AllScrewsUnscrewed()
    {
        foreach (bool unscrewed in isUnscrewed)
        {
            if (!unscrewed)
            {
                return false; // If any screw is still screwed, return false
            }
        }
        return true; // All screws are unscrewed
    }

    public void TakeUVFlashlight()
    {
        UVLightMaskScript.instance.hasFlashlightUnlocked = true; // Set the UV light mask script to indicate the UV light has been taken
        uvFlashLight.SetActive(false); // Hide the UV light GameObject
    }
}
