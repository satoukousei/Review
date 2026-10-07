using UnityEngine;
using UnityEngine.UI;

public class RingQTE : MonoBehaviour
{
    [SerializeField]
    private Transform targetA;
    [SerializeField]
    private Transform targetB;
    [SerializeField]
    private GameObject[] ring;
    [SerializeField]
    private GameObject ringJudge;
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private Image ringImage;
    [SerializeField]
    private PlayerPullData pullData;
    [SerializeField]
    private float minScale = 0.0f;
    [SerializeField]
    private float shrinkSpeed = 0;
    [SerializeField] private ObjectSideShake objectSideShake = null;
    [SerializeField] private ScreenFade redOut = null;
    [SerializeField] private SEManager seManager = null;

    private float distance = 0;
    Vector3 ringStartScale = Vector3.zero;

    private float countTime = 0;
    private bool delay = false;
    private bool isMiss = false;
    private bool isPlay = false;

    private Color defultRingColor;

    private void Start()
    {
        ring[0].SetActive(false);
        ring[1].SetActive(false);
        ringJudge.SetActive(false);
        button.SetActive(false);
        ringStartScale = ring[0].transform.localScale;
        defultRingColor = ringImage.color;
    }

    private void Update()
    {
        distance = Vector3.Distance(targetA.position, targetB.position);

        if (delay)
        {
            countTime += Time.deltaTime;
            if (countTime >= 0.5f)
            {
                delay = false;
                isMiss = false;
                isPlay = false;
                countTime = 0;
                ring[0].transform.localScale = ringStartScale;
                ringImage.color = defultRingColor;
                ring[0].SetActive(false);
                ring[1].SetActive(false);
                ringJudge.SetActive(false);
                button.SetActive(false);
            }
        }

        if (isMiss)
        {
            ring[0].SetActive(false);
            ring[1].SetActive(true);

            if (!isPlay)
            {


                objectSideShake.Shake();
                redOut.SetFadeImage();
                redOut.StartFadeOut();
                isPlay = true;
            }
        }

        ring[1].transform.localScale = ring[0].transform.localScale;
    }

    public void Ring()
    {
        ring[0].SetActive(true);
        ringJudge.SetActive(true);
        button.SetActive(true);

        float targetScale = ringStartScale.x;

        if (distance <= pullData.GetCanPullDistance())
        {
            targetScale = Mathf.Clamp01(distance / pullData.GetCanPullDistance());
            targetScale = Mathf.Max(targetScale, minScale);

            float currentScale = ring[0].transform.localScale.x;

            float index;
            if(Time.timeScale != 0)
            {
                index = 1.0f / Time.timeScale;
            }
            else
            {
                index = 1;
            }

            float newScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * shrinkSpeed * index);

            ring[0].transform.localScale = Vector3.one * newScale;

            float dis = GetScaleDistance();
            if (dis >= pullData.GetMinCatchDistance() && dis <= pullData.GetMaxCatchDistance())
            {
                Color ringColor = ringImage.color;
                ringColor.r = 140 / 255;
                ringColor.g = 255 / 255;
                ringColor.b = 0;
                ringColor.a = defultRingColor.a;
                ringImage.color = ringColor;
            }
            else
            {
                ringImage.color = defultRingColor;
            }
        }
    }
    public void SetMissRing()
    {
        if (isMiss)
        {
            return;
        }

        isMiss = true;
        seManager.PlayPlayerCatchFailSE(); //キャッチ失敗SEを再生する
    }
    public void RingReset()
    {
        delay = true;
    }
    public float GetScaleDistance()
    {
        return ring[0].transform.localScale.x;
    }
}

