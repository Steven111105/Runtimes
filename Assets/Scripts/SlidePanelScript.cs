using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePanelScript : MonoBehaviour
{
    Transform content;
    // Start is called before the first frame update
    void Start()
    {
        content = transform.GetChild(0).GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (content.localPosition.x > -922.5f)
        {
            // Debug.Log(content.localPosition.x);
            ComputerCodeInput.instance.SwitchScreen();
            SliderScript.instance.CancelSlider();
            Destroy(gameObject);
        }
    }
}
