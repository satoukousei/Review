using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LowHpScreenEffect : MonoBehaviour
{
    [SerializeField] private Gage gage = null;
    [SerializeField] private GameObject fadeObject = null;
    [SerializeField] private Image fadeImage = null;
    [SerializeField,Range(0,1)] private float MaxAlpha = 0;
    [SerializeField,Range(0,10)] private float fadeSpeed = 0;

    private bool isPlay = false;

    private float hp = 0;
    private float startAlpha = 0;
    private float fadeTimer = 0f;

    private void Start()
    {
        fadeObject.SetActive(false);
        startAlpha = MaxAlpha;
        Color fadeColor = fadeImage.color;
        fadeColor.a = MaxAlpha;
        fadeImage.color = fadeColor;
    }

    private void Update()
    {
        CheckHp();
        Fade();
    }
    private void CheckHp()
    {
        hp = gage.GetNowGage();

        if (hp <= 33)
        {
            isPlay = true;
        }
        else
        {
            isPlay= false;
        }
    }
    private void Fade()
    {
        if (isPlay)
        {
            if (!fadeObject.activeSelf)
            {
                fadeObject.SetActive(true);
            }

            fadeTimer += Time.deltaTime * fadeSpeed;

            Color color = fadeImage.color;
            color.a = Mathf.PingPong(fadeTimer, MaxAlpha);
            fadeImage.color = color;
        }
        else
        {
            if (fadeObject.activeSelf)
            {
                fadeObject.SetActive(false);
            }

            fadeTimer = 0f;

            Color color = fadeImage.color;
            color.a = startAlpha;
            fadeImage.color = color;
        }
    }
}