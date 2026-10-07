using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonAnimation : MonoBehaviour
{
    [Header("対象Image")]
    [SerializeField] private RectTransform buttonImage = null;
    [SerializeField] private GameObject buttonObject = null;
    [Header("最大サイズ")]
    [SerializeField] private float maxScale = 0;
    [Header("最小サイズ")]
    [SerializeField] private float minScale = 0;
    [Header("アニメーション速度")]
    [SerializeField] private float speed = 0;

    private Vector3 baseScale = Vector3.zero;
    private bool isPlaying = false;
    private void Start()
    {
        baseScale = buttonImage.localScale;
        buttonObject.SetActive(false);
    }

    private void Update()
    {
        ScaleChange();
    }
    private void ScaleChange()
    {
        if (isPlaying)
        {
            float t = Mathf.PingPong(Time.time * speed, 1f);
            float scale = Mathf.Lerp(minScale, maxScale, t);

            buttonImage.localScale = baseScale * scale;
        }
    }
    
    public void Play()
    {
        if (isPlaying)
        {
            return;
        }
        isPlaying = true;
        buttonObject.SetActive(true);
    }
    public void Stop()
    {
        isPlaying = false;
        buttonImage.localScale = baseScale;
        buttonObject.SetActive(false);
    }
}