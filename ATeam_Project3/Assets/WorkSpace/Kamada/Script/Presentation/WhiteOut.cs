using UnityEngine;
using UnityEngine.UI;

public class WhiteOut : MonoBehaviour
{
    [SerializeField] private GameObject whiteObject = null;
    [SerializeField] private Image whiteImage = null;
    [SerializeField] private float duration = 0;
    [SerializeField] private float maxAlpha = 0;
    [SerializeField] private MovieManager movieManager = null;

    private Color startColor = Color.white;
    private float timer = 0f;
    private bool isPlay = false;
    private bool isStop = false;

    private void Start()
    {
        whiteImage.color = new Color(whiteImage.color.r, whiteImage.color.g, whiteImage.color.b,0);
        startColor = whiteImage.color;
        whiteObject.SetActive(false);
    }

    private void Update()
    {
        if (movieManager.GetIsStop())
        {
            PlayWhiteOut();
        }


        if (!isPlay) return;

        timer += Time.deltaTime;

        float t = timer / duration;
        t = Mathf.Clamp01(t);

        Color color = whiteImage.color;
        color.a = Mathf.Lerp(startColor.a, maxAlpha, t);
        whiteImage.color = color;

        if (t >= duration)
        {
            isPlay = false;
            whiteObject.SetActive(false);
            isStop = true;
        }
    }

    public void PlayWhiteOut()
    {
        if (isPlay || isStop)
        {
            return;
        }
        whiteObject.SetActive(true);
        timer = 0f;
        whiteImage.color = startColor;
        isPlay = true;
    }
}