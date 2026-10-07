using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    [SerializeField] private Renderer itemRend = null;
    [SerializeField] private float startTime = 0;
    [SerializeField] private float switchInterval = 0;

    private float countTime = 0;
    private float showCount = 0;
    private float hideCount = 0;
    private float index = 1;

    private bool isPlay = false;
    private bool isBlink = false;
    private bool isShow = false;
    private bool isHide = false;
    private bool isEnd = false;
    private void Update()
    {
        TimeCount();
        Show();
        Hide();
    }
    private void TimeCount()
    {
        if (!isPlay)
        {
            return;
        }

        countTime -= Time.deltaTime * SlowCheck();

        if (!isBlink && countTime <= startTime)
        {
            isHide = true;
            isBlink = true;
        }

        if(countTime <= 0)
        {
            isPlay = false;
            isEnd = true;
        }
    }
    private void Show()
    {
        if (!isShow)
        {
            return;
        }

        showCount += Time.deltaTime * SlowCheck();
        if(showCount >= switchInterval)
        {
            showCount = 0;
            itemRend.enabled = false;
            isShow = false;
            isHide = true;
        }
    }
    private void Hide()
    {
        if (!isHide)
        {
            return;
        }

        hideCount += Time.deltaTime * SlowCheck();
        if(hideCount >= switchInterval)
        {
            hideCount = 0;
            itemRend.enabled = true;
            isHide = false;
            isShow = true;
        }
    }
    private float SlowCheck()
    {
        if (Time.timeScale != 1)
        {
            index = 1.0f / Time.timeScale;
        }
        else
        {
            index = 1.0f;
        }

        return index;
    }
    public void PlayTimeCount(float time)
    {
        if (isPlay)
        {
            return;
        }

        countTime = time;
        isBlink = false;
        isShow = false;
        isHide = false;
        isPlay = true;
    }
    public bool GetIsEnd()
    {
        return isEnd;
    }
}