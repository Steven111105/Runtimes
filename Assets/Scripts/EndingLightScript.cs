using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingLightScript : MonoBehaviour
{
    public Sprite onLightSprite;
    public Sprite offLightSprite;

    void Start()
    {
        TurnOnLights();
    }

    public void TurnOnLights()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            switch (i)
            {
                case 0:
                    //Tea Time
                    if(PlayerPrefs.GetInt("TeaTimeEnding") == 1)
                    transform.GetChild(i).GetComponent<Image>().sprite = onLightSprite;
                    break;
                case 1:
                    //Self Destruct
                    if(PlayerPrefs.GetInt("SelfDestructEnding") == 1)
                        transform.GetChild(i).GetComponent<Image>().sprite = onLightSprite;
                    break;
                case 2:
                    //War Starter (Anagram)
                    if(PlayerPrefs.GetInt("WarStarterEnding") == 1)
                        transform.GetChild(i).GetComponent<Image>().sprite = onLightSprite;
                    break;
                case 3:
                    //Too many attempts
                    if(PlayerPrefs.GetInt("TooManyAttemptsEnding") == 1)
                        transform.GetChild(i).GetComponent<Image>().sprite = onLightSprite;
                    break;
                case 4:
                    //You did nothing
                    if(PlayerPrefs.GetInt("YouDidNothingEnding") == 1)
                        transform.GetChild(i).GetComponent<Image>().sprite = onLightSprite;
                    break;
                case 5:
                    //Running Away (Runtimes)
                    if(PlayerPrefs.GetInt("RunningAwayEnding") == 1)
                        transform.GetChild(i).GetComponent<Image>().sprite = onLightSprite;
                    break;
                case 6:
                    //Pesky Button
                    if(PlayerPrefs.GetInt("PeskyButtonEnding") == 1)
                        transform.GetChild(i).GetComponent<Image>().sprite = onLightSprite;
                    break;
                case 7:
                    //Terminus
                    if(PlayerPrefs.GetInt("TerminusEnding") == 1)
                        transform.GetChild(i).GetComponent<Image>().sprite = onLightSprite;
                    break;
            }
        }
    }
}
