using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour
{
    public TMP_Text selfDestructText; // Reference to the text component for self-destruct messages
    public GameObject screen1;
    public GameObject screen2;
    public int amountPressed = 0;
    bool isSelfDestructing = false;
    // Start is called before the first frame update
    void Start()
    {
        amountPressed = 0;
        isSelfDestructing = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pressed()
    {
        if (isSelfDestructing) return; // Prevent further presses if self-destruct is already initiated
        SoundInstance.Instance.PlayClick(); // Play button press sound
        amountPressed++;
        if (amountPressed >= 5)
        {
            EndScreenScript.instance.BlockInput();
            StartCoroutine(SelfDestructCountdown());
            screen1.SetActive(false);
            screen2.SetActive(false);
            selfDestructText.gameObject.SetActive(true);
            Debug.Log("Self Destruct Ending");
        }
    }

    private IEnumerator SelfDestructCountdown()
    {
        if (isSelfDestructing) yield break; ;
        isSelfDestructing = true;
        int tempCountdown = 5;
        while (true)
        {
            selfDestructText.text = $"Self Destruct Sequence Initiated \n{tempCountdown}";
            yield return new WaitForSeconds(1f);
            tempCountdown--;
            if (tempCountdown < 0)
            {
                Debug.Log("Self Destruct Ending");
                EndScreenScript.instance.ShowEndScreen(1); // Show the self-destruct ending
                yield break;
            }
        }
    }
}
