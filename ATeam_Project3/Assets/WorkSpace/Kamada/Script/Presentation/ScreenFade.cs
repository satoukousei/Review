using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeObj; // フェード用オブジェクト
    [SerializeField]
    private Image fadeImage; // マテリアルのアルファを変える対象

    private bool fadeOut = false;
    private float time = 0; // フェードにかかる時間
    private float startColorA = 0;

    private void Start()
    {
        fadeObj.SetActive(false);
        startColorA = fadeImage.color.a;
        time = startColorA;
    }
    private void Update()
    {
        if (fadeOut)
        {
            time -= Time.deltaTime;
            Color color = fadeImage.color;
            color.a = time;
            fadeImage.color = color;

            if (time <= 0.0f)
            {
                fadeOut = false;
                fadeObj.SetActive(false);
                color.a = startColorA;
                fadeImage.color = color;
            }
        }

    }
    public void SetFadeImage()
    {
        fadeObj.SetActive(true);
    }

    public void StartFadeOut()
    {
        fadeOut = true;
        time = startColorA;
    }
}