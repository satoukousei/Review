using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private float duration = 1.0f;   // 消えるまでの時間
    [SerializeField] private float maxScale = 1.5f;   // 最終的な大きさ
    [SerializeField] private GameObject imageObject;
    [SerializeField] private Image image;
    private Vector3 startScale;
    private float timer;

    private bool isFade = false;
    private void Start()
    {
        imageObject.SetActive(false);
        startScale = transform.localScale;
        timer = 0f;
    }

    private void Update()
    {
        if (isFade)
        {
            FadeAndScaleUpUI();
        }
    }

    private void FadeAndScaleUpUI()
    {
        timer += Time.deltaTime * (1 / Time.timeScale);
        float t = timer / duration;

        // サイズ拡大（Lerp）
        transform.localScale = Vector3.Lerp(startScale, new Vector3(maxScale, maxScale, maxScale), t);

        // フェードアウト
        Color c = image.color;
        c.a = Mathf.Lerp(1f, 0f, t);
        image.color = c;

        // 完全に消えたら削除
        if (t >= 1f)
        {
            isFade = false;
            ResetUI();
        }
    }

    private void ResetUI()
    {
        transform.localScale = startScale;
        timer = 0;
        imageObject.SetActive(false);
    }

    public void PlayFade()
    {
        isFade = true;
        imageObject.SetActive(true);
    }
}
