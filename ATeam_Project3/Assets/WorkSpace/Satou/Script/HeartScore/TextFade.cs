using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;



public class TextFade : MonoBehaviour
{
    [SerializeField]private float fadeTime = 1.0f;
    private bool isFading = false;
    [SerializeField]private TextMeshProUGUI text = null;

    private Coroutine fadeIn = null;
    private Coroutine fadeOut = null;
    public float GetFadeTime()
    {
        return fadeTime;
    }

    public void FadeIn()
    {
        if (isFading)
        {
            return;
        }
        isFading = true;
        fadeIn = StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        if (isFading)
        {
            return;
        }
        isFading = true;
        fadeOut = StartCoroutine(FadeOutCoroutine());
    }
    public void StopFade()
    {
        if (fadeIn != null)
        {
            StopCoroutine(fadeIn);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            fadeIn = null;
        }
        isFading = false;
    }
    public void SetFadeTime(float time)
    {
        fadeTime = time;
    }
    private IEnumerator FadeOutCoroutine()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        float time = 0.0f;
        Color color = text.color;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            color.a = time / fadeTime;
            text.color = color;
            yield return null;
        }
        isFading = false;
    }
    private IEnumerator FadeInCoroutine()
    {
        isFading = true;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        float time = 0.0f;
        Color color = text.color;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            color.a = 1 - (time / fadeTime);
            text.color = color;
            yield return null;
        }
        text.text = "";
        isFading = false;
    }
}
