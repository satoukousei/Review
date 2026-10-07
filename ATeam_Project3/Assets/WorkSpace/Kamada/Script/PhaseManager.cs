using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private GameObject[] phaseObject = null;
    [SerializeField] private Image[] phaseImage = null;
    [SerializeField] private Image[] textImage = null;
    [SerializeField] private float showTime = 0;
    [SerializeField] private float hideTime = 0;

    private int phaseNumber = 0;

    private float countTime = 0;
    private float fadeTimer = 0;

    private bool isPlay = false;
    private bool isShow  = false;
    private bool isHide = false;
    private bool isEnd = false;
    private void Start()
    {
        for (int i = 0; i < phaseObject.Length; i++)
        {
            phaseImage[i].color = new Color(phaseImage[i].color.r, phaseImage[i].color.g, phaseImage[i].color.b,0);
            textImage[i].color = phaseImage[i].color;
            phaseObject[i].SetActive(false);
        }
        isPlay = false;
        isShow = false;
        isHide = false;
    }
    private void Update()
    {
        ShowCount();
        Show();
        Hide();
    }
    private void ShowCount()
    {
        if (!isPlay)
        {
            return;
        }

        countTime -= Time.deltaTime;

        if(countTime <= 0)
        {
            isPlay = false;
            isHide = true;
        }

    }
    private void Show()
    {
        if (!isShow)
        {
            return;
        }

        fadeTimer += Time.deltaTime;

        float alpha = fadeTimer / showTime;
        alpha = Mathf.Clamp01(alpha);

        Color color = phaseImage[phaseNumber].color;
        color.a = alpha;
        phaseImage[phaseNumber].color = color;
        textImage[phaseNumber].color = phaseImage[phaseNumber].color;

        if (fadeTimer >= showTime)
        {
            isShow = false;
            isPlay = true;
            fadeTimer = 0f;
        }
    }
    private void Hide()
    {
        if (!isHide)
        {
            return;
        }

        fadeTimer += Time.deltaTime;

        float alpha = 1f - (fadeTimer / hideTime);
        alpha = Mathf.Clamp01(alpha);

        Color color = phaseImage[phaseNumber].color;
        color.a = alpha;
        phaseImage[phaseNumber].color = color;
        textImage[phaseNumber].color = phaseImage[phaseNumber].color;

        if (fadeTimer >= hideTime)
        {
            isHide = false;
            phaseObject[phaseNumber].SetActive(false);
            fadeTimer = 0f;
            if(!isEnd && phaseNumber == 3)
            {
                isEnd = true;
            }
        }
    }
    public void SetPhase(int _phaseNumber,float time)
    {
        phaseNumber = _phaseNumber;
        phaseObject[phaseNumber].SetActive(true);
        Color color = phaseImage[phaseNumber].color;
        color.a = 0.0f;
        phaseImage[phaseNumber].color = color;
        textImage[phaseNumber].color = phaseImage[phaseNumber].color;
        countTime = time;
        fadeTimer = 0f;
    }
    public void PlayPhase()
    {
        isShow = true;
    }
    public bool GetIsEnd() { return isEnd; }
}