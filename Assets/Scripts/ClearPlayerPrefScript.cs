using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPrefScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.C))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs Cleared");
        }
    }
}
