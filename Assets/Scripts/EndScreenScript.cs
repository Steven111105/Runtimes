using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    public EndScreenData[] endingsData;
    public static EndScreenScript instance;
    public GameObject blockInputPanel;
    public TMP_Text endScreenLoreText;
    public TMP_Text endingText;
    public TMP_Text terminusEndingText;
    public Button restartButton;
    Image endScreenBG;

    public GameObject middleScreen;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            endScreenBG = GetComponent<Image>();
            blockInputPanel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowEndScreen(int endingIndex)
    {
        BlockInput();
        gameObject.SetActive(true);
        StartCoroutine(DisplayEndScreen(endingIndex));
        switch (endingIndex)
        {
            case 0:
                PlayerPrefs.SetInt("TeaTimeEnding", 1);
                break;
            case 1:
                PlayerPrefs.SetInt("SelfDestructEnding", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("WarStarterEnding", 1);
                break;
            case 3:
                PlayerPrefs.SetInt("TooManyAttemptsEnding", 1);
                break;
            case 4:
                PlayerPrefs.SetInt("YouDidNothingEnding", 1);
                break;
            case 5:
                PlayerPrefs.SetInt("RunningAwayEnding", 1);
                break;
            case 6:
                PlayerPrefs.SetInt("PeskyButtonEnding", 1);
                break;
            case 7:
                PlayerPrefs.SetInt("TerminusEnding", 1);
                break;
        }
    }

    public void CallAnimation(string endingName)
    {
        foreach (Transform child in middleScreen.transform)
        {
            child.gameObject.SetActive(false);
        }
        middleScreen.GetComponent<Animator>().Play(endingName);
    }

    private IEnumerator DisplayEndScreen(int endingIndex)
    {
        float d = 0;
        float duration = 1.5f;
        restartButton.gameObject.SetActive(false);
        endScreenLoreText.text = "";
        endingText.text = "";
        endScreenBG.color = new Color(0, 0, 0, 0);
        restartButton.interactable = false;
        while (d < duration)
        {
            d += Time.deltaTime;
            endScreenBG.color = new Color(0, 0, 0, d / duration);
            yield return null;
        }

        EndScreenData endingData = endingsData[endingIndex];
        if (endingIndex == 7)
        {
            endingData.loreText = System.Text.RegularExpressions.Regex.Unescape(endingData.loreText);
        }

        foreach (char c in endingData.loreText)
        {
            if (endingIndex == 7)
            {
                terminusEndingText.text += c;
            }
            else
            {
                endScreenLoreText.text += c;
            }
            yield return new WaitForSeconds(0.05f);
        }

        foreach (char c in endingData.endingText)
        {
            endingText.text += c;
            yield return new WaitForSeconds(0.07f);
        }
        TMP_Text restartText = restartButton.GetComponentInChildren<TMP_Text>();
        Color targetColor = restartText.color;
        restartText.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0);
        while (restartText.color.a < 1)
        {
            restartText.color = new Color(targetColor.r, targetColor.g, targetColor.b, restartText.color.a + Time.deltaTime);
            yield return null;
        }

        restartButton.gameObject.SetActive(true);
        restartButton.interactable = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BlockInput()
    {
        blockInputPanel.SetActive(true);
    }
    
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}

[System.Serializable]
public class EndScreenData
{
    public string loreText;
    public string endingText;
}
