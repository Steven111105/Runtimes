using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComputerScreenClick : MonoBehaviour
{
    public int clickedAmount = 0;
    public int buttonClickedAmount = 0;

    [SerializeField] TMP_Text computerText;
    [SerializeField] Button computerButton;
    TMP_Text buttonText;
    [SerializeField] Vector2[] keyboardPositions;

    private void Start()
    {
        computerText.text = "Press Start to";
        computerText.gameObject.SetActive(true);
        // computerButton.gameObject.SetActive(false);

    }
    public void OnClick()
    {
        SliderScript.instance.CancelSlider();
        clickedAmount++;
        if (clickedAmount == 5)
        {
            ButtonPhase();
        }
        else
        {
            computerText.text += " .";
        }
    }

    private void ButtonPhase()
    {
        buttonClickedAmount = 0;
        computerText.gameObject.SetActive(false);
        computerButton.gameObject.SetActive(true);
        buttonText = computerButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = "?";
    }

    public void ButtonClick()
    {
        if (buttonClickedAmount < 8)
        {
            Debug.Log("Spelling anagram");
            if (buttonClickedAmount == 0)
            {
                buttonText.text = "A";
            }
            else
            {
                buttonText.text = "?";
            }
            computerButton.transform.localPosition = keyboardPositions[buttonClickedAmount];
        }
        else if (buttonClickedAmount == 8)
        {
            Debug.Log("Done spelling anagram");
            buttonText.text = "!";
            computerButton.transform.localPosition = keyboardPositions[8];
        }
        else
        {
            //call end game screen
            Debug.Log("End game screen should be called here.");
            Debug.Log("Pesky Button Ending");
            EndScreenScript.instance.ShowEndScreen(0); // Example usage, replace 0 with the desired ending index
        }
        buttonClickedAmount++;
    }
}
