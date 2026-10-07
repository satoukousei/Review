using UnityEngine;
using UnityEngine.InputSystem;

public class StageRubbles : MonoBehaviour
{
    [Header("瓦礫オブジェクト")]
    [SerializeField] private GameObject stageRubbles = null;
    [Header("瓦礫オブジェクトのリジッドボディ")]
    [SerializeField] private Rigidbody rb = null;
    [Header("オブジェクトのマテリアル")]
    [SerializeField] private Renderer[] rend = null;
    [Header("移動前のY座標")]
    [SerializeField] private float startPosY = 0;
    [Header("移動後のY座標")]
    [SerializeField] private float endPosY = 0;

    [Header("表示時間")]
    [SerializeField] private float displayTime = 0;
    [Header("フェード時間")]
    [SerializeField] private float fadeDuration = 0;

    private float countTime = 0;
    private float fadeTimer = 0f;
    private float spawnTimer = 0f;

    private bool isPlay = false;
    private bool isDestroy = false;
    private bool isMove = false;
    private void Update()
    {
        if (!isPlay)
        {
            return;
        }

        CountDisplayTime();
        DestroyRubbles();
        MoveStageRubbles();
    }
    private void MoveStageRubbles()
    {
        if (!isMove)
        {
            return;
        }

        spawnTimer += Time.deltaTime;
        float t = spawnTimer / 1.0f;
        Vector3 pos = rb.position;
        rb.position = Vector3.Lerp(new Vector3(pos.x, startPosY, pos.z),new Vector3(pos.x, endPosY, pos.z),t);

        if(rb.position.y >= -1.05f)
        {
            isMove = false;
        }
    }
    private void CountDisplayTime()
    {
        countTime += Time.deltaTime;
        if(countTime >= displayTime - fadeDuration)
        {
            isDestroy = true;
        }
    }
    private void DestroyRubbles()
    {
        if (!isDestroy)
        {
            return;
        }

        fadeTimer += Time.deltaTime;
        float t = fadeTimer / fadeDuration;

        for (int i = 0; i < rend.Length; i++)
        {
            float alpha = Mathf.Lerp(1f, 0f, t);
            rend[i].material.color = new Color(
                rend[i].material.color.r,
                rend[i].material.color.g,
                rend[i].material.color.b,
                alpha
                );
        }

        if (fadeTimer >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
    public void Play()
    {
        isPlay = true;
        isMove = true;
        stageRubbles.SetActive(true);
    }
}