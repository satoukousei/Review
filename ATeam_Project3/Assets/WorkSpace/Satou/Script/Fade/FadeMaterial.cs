using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeMaterial : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeObj; // フェード用オブジェクト
    [SerializeField]
    private MeshRenderer fadeImage; // マテリアルのアルファを変える対象
    
    private bool fadeIn = false;
    private bool fadeOut = false;
    private float time = 1.0f; // フェードにかかる時間

    private IEnumerator FadeIn()
    {
        float alfa = fadeImage.material.color.a;
        alfa = 1.0f;
        // ScriptableObject の aoeDelayTime を使用してフェード速度を決定
        float fadeSpeed = Time.deltaTime/ time;
        fadeIn = true;
        while (alfa > 0)
        {
            alfa -= fadeSpeed;            
            Alpha(alfa);
            yield return null;
        }
        alfa = 0;
        fadeIn = false;
        fadeObj.SetActive(false);
    }

    private IEnumerator FadeOut()
    {
        float alfa = fadeImage.material.color.a;
        alfa = 0.0f;
        // ScriptableObject の aoeDelayTime を使用してフェード速度を決定
        float fadeSpeed = Time.deltaTime/ time;
        fadeObj.SetActive(true);
        fadeOut = true;
        while (alfa < 1)
        {
            alfa += fadeSpeed;            
            Alpha(alfa);
            yield return null;
        }
        alfa = 0.0f;
        Alpha(alfa);
        fadeOut = false;
    }

    void Alpha(float a)
    {
        Color c = fadeImage.material.color;
        fadeImage.material.color = new Color(c.r, c.g, c.b, a);
    }

    public void OnFadeOut(float fadeTime)
    {
        time = fadeTime;
        StartCoroutine(FadeOut());
    }
    public void OnFadeIn(float fadeTime)
    {
        time = fadeTime;
        StartCoroutine(FadeIn());
    }
    public bool GetFadeIn()
    {
        return fadeIn;
    }
    public bool GetFadeOut()
    {
        return fadeOut;
    }
}
