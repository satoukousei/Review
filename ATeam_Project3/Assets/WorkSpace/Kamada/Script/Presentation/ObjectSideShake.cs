using System.Collections;
using UnityEngine;

public class ObjectSideShake : MonoBehaviour
{
    [Header("揺れの強さ（左右の幅）")]
    [SerializeField] private float shakeAmount = 20f; // UIはピクセルなので大きめ

    [Header("揺れの時間")]
    [SerializeField] private float shakeDuration = 0.2f;

    [Header("揺れの速さ")]
    [SerializeField] private float shakeSpeed = 25f;

    private RectTransform rect;
    private Vector2 originalPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Mathf.Sin(elapsed * shakeSpeed) * shakeAmount;
            rect.anchoredPosition = originalPos + new Vector2(x, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = originalPos;
    }
}