using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour
{
    public int amountPressed = 0;
    // Start is called before the first frame update
    void Start()
    {
        amountPressed = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Pressed()
    {
        amountPressed++;
        if (amountPressed >= 5)
        {
            Debug.Log("Self Destruct Ending");
            EndScreenScript.instance.ShowEndScreen(1); // Show the self-destruct ending
        }
    }
}
