using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public static SliderScript instance;
    public Slider slider;
    Image sliderBG;
    Image sliderFill;

    private void Start()
    {
        instance = this;
        slider.maxValue = 30;
        slider.value = 30;
        sliderBG = slider.transform.GetChild(0).GetComponent<Image>();
        sliderFill = slider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        StartCoroutine(SliderAppearTimer());
    }

    public void CancelSlider()
    {
        StopAllCoroutines();
        sliderBG.color = new Color(sliderBG.color.r, sliderBG.color.g, sliderBG.color.b, 0f);
        sliderFill.color = new Color(sliderFill.color.r, sliderFill.color.g, sliderFill.color.b, 0f);
        slider.gameObject.SetActive(false);
    }

    private IEnumerator SliderAppearTimer()
    {
        yield return new WaitForSeconds(29f);
        sliderBG.color = new Color(sliderBG.color.r, sliderBG.color.g, sliderBG.color.b, 0f);
        sliderFill.color = new Color(sliderFill.color.r, sliderFill.color.g, sliderFill.color.b, 0f);
        slider.gameObject.SetActive(true);
        float fadeDuration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            sliderBG.color = new Color(sliderBG.color.r, sliderBG.color.g, sliderBG.color.b, alpha);
            sliderFill.color = new Color(sliderFill.color.r, sliderFill.color.g, sliderFill.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(DrainSlider());
    }

    private IEnumerator DrainSlider()
    {
        while (slider.value > 0)
        {
            slider.value -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("Tired Already? Ending");
        EndScreenScript.instance.ShowEndScreen(2);
    }
}
