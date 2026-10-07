using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [Header("自分")]
    [SerializeField] private Transform playerHero = null;
    [Header("相手")]
    [SerializeField] private Transform playerWomen = null;
    [Header("ヘルプUI")]
    [SerializeField] private RectTransform helpTr = null;
    [Header("ヘルプUI")]
    [SerializeField] private GameObject helpObj = null;
    [Header("引っ張る側のUI")]
    [SerializeField] private RectTransform pullPromptUITr = null;
    [Header("引っ張る側のUI")]
    [SerializeField] private GameObject pullPromptUIObj = null;
    [Header("上側の表示位置")]
    [SerializeField] private float upperOffset = 0.0f;
    [Header("下側の表示位置")]
    [SerializeField] private float lowerOffset = 0.0f;
    [Header("表示位置を変えるZ座標")]
    [SerializeField] private float ChangePosZ = 0.0f;
    [Header("HelpUIの拡大縮小")]
    [SerializeField] private Expand_Shrink shrink = null;
    [Header("Aボタンの拡大縮小")]
    [SerializeField] private Expand_Shrink targetShrink = null;
    [Header("ヘルプフェード")]
    [SerializeField]
    private HeartFade helpFade = null;
    [Header("Aボタンのフェード")]
    [SerializeField]
    private HeartFade buttonFade = null;

    private Camera mainCamera = null;

    private void Start()
    {
        mainCamera = Camera.main;
        helpObj.SetActive(false);
        pullPromptUIObj.SetActive(false);
    }
    private void Update()
    {
        if(playerHero != null)
        {
            bool heroUpper = PlayerPositionCheck(playerHero.position);
            helpTr.position = SetUIPosition(playerHero.position, heroUpper);
        }

        if (playerWomen != null)
        {
            bool womenUpper = PlayerPositionCheck(playerWomen.position);
            pullPromptUITr.position = SetUIPosition(playerWomen.position, womenUpper);
        }
    }

    private bool PlayerPositionCheck(Vector3 pos)
    {
        return pos.z >= ChangePosZ;
    }

    private Vector3 SetUIPosition(Vector3 targetPos,bool flag)
    {
        Vector3 worldPos;
        if (flag)
        {
            worldPos = targetPos + Vector3.down * lowerOffset;
        }
        else
        {
            worldPos = targetPos + Vector3.up * upperOffset;
        }

        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        return screenPos;
    }

    public void SetUIActive(bool flag)
    {
        if (flag)
        {
            helpObj.SetActive(flag);
            pullPromptUIObj.SetActive(flag);
            PlayUIAnimation();
        }
        else
        {
            StopUIAnimation();
        }
    }

    private void PlayUIAnimation()
    {
        helpFade.FadeOut();
        buttonFade.FadeOut();
        shrink.PlayExpandShrink();
        targetShrink.PlayExpandShrink();
    }

    private void StopUIAnimation()
    {
        helpFade.FadeIn();
        buttonFade.FadeIn();
        shrink.StopExpandShrink();
        targetShrink.StopExpandShrink();
    }
}