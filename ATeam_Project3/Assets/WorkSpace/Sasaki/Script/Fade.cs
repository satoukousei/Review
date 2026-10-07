using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

enum NextSceneType
{
    TitleScene,
    MainScene,
    GameClerScene,
    GameOverScene
}

public class Fade : MonoBehaviour
{
    [SerializeField]private float fadeTime = 1.0f;
    private bool isFading = false;
    [SerializeField]private Image image = null;
    private void Start()
    {    }

    public void FadeIn()
    {
        if (isFading)
        {
            return;
        }
        isFading = true;
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        if (isFading)
        {
            return;
        }
        isFading = true;
        StartCoroutine(FadeOutCoroutine());
    }

    public void FadeOut(string sceneName)
    {
        if (isFading)
        {
            return;
        }
        StartCoroutine(FadeOutCoroutine(sceneName));
    }

    public void SetFadeTime(float time)
    {
        fadeTime = time;
    }
    private IEnumerator FadeOutCoroutine()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        float time = 0.0f;
        Color color = image.color;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            color.a = time / fadeTime;
            image.color = color;
            yield return null;
        }
        isFading = false;
    }
    private IEnumerator FadeOutCoroutine(string sceneName)
    {
        isFading = true;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        float time = 0.0f;
        Color color = image.color;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            color.a = time / fadeTime;
            image.color = color;
            yield return null;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        isFading = false;
    }
    private IEnumerator FadeInCoroutine()
    {
        isFading = true;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        float time = 0.0f;
        Color color = image.color;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            color.a = 1 - (time / fadeTime);
            image.color = color;
            yield return null;
        }
        isFading = false;
        this.gameObject.SetActive(false);
    }
}
