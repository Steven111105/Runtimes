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
    public TMP_Text endScreenLoreText;
    public TMP_Text endingText;
    public Button restartButton;
    Image endScreenBG;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            endScreenBG = GetComponent<Image>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowEndScreen(0); // Example usage, replace 0 with the desired ending index
        }
    }

    public void ShowEndScreen(int endingIndex)
    {
        gameObject.SetActive(true);
        StartCoroutine(DisplayEndScreen(endingIndex));
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

        foreach (char c in endingData.loreText)
        {
            endScreenLoreText.text += c;
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
