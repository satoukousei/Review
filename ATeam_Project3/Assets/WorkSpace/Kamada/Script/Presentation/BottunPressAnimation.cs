using UnityEngine;

public class ButtonPressAnimation : MonoBehaviour
{
    [Header("対象Image")]
    [SerializeField] private RectTransform buttonImage = null;
    [SerializeField] private GameObject buttonObject = null;
    [Header("縮小速度")]
    [SerializeField] private float scaleDownSpeed = 0;
    [Header("拡大速度")]
    [SerializeField] private float scaleUpSpeed = 0;
    [Header("最大サイズ")]
    [SerializeField] private float maxScale = 0;
    [Header("最小サイズ")]
    [SerializeField] private float minScale = 0;
    [Header("成功時の円")]
    [SerializeField] private FadeUI fadeUI = null;

    private Vector3 baseScale = Vector3.zero;

    private bool isPlay = false;
    private bool isScaleDown = false;
    private bool isScaleUp = false;

    private void Start()
    {
        baseScale = buttonImage.localScale;
        buttonObject.SetActive(false);
    }
    private void Update()
    {
        if (isPlay)
        {
            ScaleDown();
            ScaleUp();
        }
        else
        {
            if (buttonObject.activeSelf)
            {
                buttonObject.SetActive(false);
                buttonImage.localScale = baseScale;
            }
        }
    }
    private void ScaleDown()
    {
        if (isScaleDown)
        {
            float scale = buttonImage.localScale.x;
            scale -= scaleDownSpeed * Time.deltaTime;
            buttonImage.localScale = new Vector3(scale, scale, scale);

            if(buttonImage.localScale.x <= minScale + 0.1f)
            {
                isScaleDown = false;
                isScaleUp = true;
            }
        }
    }
    private void ScaleUp()
    {
        if (isScaleUp)
        {
            float scale = buttonImage.localScale.x;
            scale += scaleUpSpeed * Time.deltaTime;
            buttonImage.localScale = new Vector3(scale, scale, scale);

            if(buttonImage.localScale.x >= maxScale - 0.1f)
            {
                isScaleUp = false;
                isPlay = false;
            }
        }
    }
    public void PlayPushButton()
    {
        if (isPlay)
        {
            return;
        }
        isPlay = true;
        isScaleDown = true;
        buttonObject.SetActive(true);
        fadeUI.PlayFade();
    }
}