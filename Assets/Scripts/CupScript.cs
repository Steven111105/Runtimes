using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupScript : MonoBehaviour
{
    public bool isFilled = false; // Flag to check if the cup is filled
    public Sprite filledSprite; // Sprite to show when the cup is filled
    // Start is called before the first frame update
    void Start()
    {
        isFilled = false; // Initialize cup as empty
    }
    public void FillCup()
    {
        if (!isFilled)
        {
            isFilled = true; // Set the cup as filled
            GetComponent<Image>().sprite = filledSprite; // Change the sprite to filled
            Debug.Log("Cup is now filled.");
        }
    }

    public void DrinkTea()
    {
        if (!isFilled) return;
        Debug.Log("Tea Time Ending");
        EndScreenScript.instance.ShowEndScreen(1); // Show the end screen with index 1 for drinking tea
    }
}
