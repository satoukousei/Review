using UnityEngine;

public class MoveToUI : MonoBehaviour
{
    [Header("目標UI")]
    [SerializeField] private RectTransform[] targetUI;
    [Header("UIに向かって動かす3Dオブジェクト")]
    [SerializeField] private GameObject moveObject;
    [Header("移動時間（秒）")]
    [SerializeField] private float moveDuration = 0.5f;
    [Header("最小サイズ")]
    [SerializeField] private float minScale = 0.2f;
    [Header("追従停止用")]
    [SerializeField] private ItemTracking itemTracking;
    [Header("目標UI番号")]
    [SerializeField] private int targetIndex = 0;
    [Header("アイテム番号")]
    [SerializeField] private int itemNumber = 0;
    [Header("プレイヤー判定")]
    [SerializeField] private PlayerPull player1;
    [SerializeField] private PlayerPull player2;

    private Camera mainCamera;

    private Vector3 startPos;
    private Vector3 targetWorldPos;
    private Vector3 controlPoint1;
    private Vector3 controlPoint2;
    private Vector3 startScale;

    private float moveTime;
    private float slowMoveDuration;
    private float fastMoveDuration;

    private bool isMoving = false;

    public System.Action OnArrived;

    private void Start()
    {
        mainCamera = Camera.main;

        startScale = moveObject.transform.localScale;
        moveObject.SetActive(false);

        slowMoveDuration = moveDuration;
        fastMoveDuration = moveDuration / 2.5f;
    }

    private void Update()
    {
        if (!isMoving) return;

        // UI → World を毎フレーム更新
        UpdateTargetWorldPos();

        // 速度切り替え
        moveDuration = (player1.GetIsCatch() || player2.GetIsCatch())
            ? fastMoveDuration
            : slowMoveDuration;

        moveTime += Time.deltaTime;
        float t = Mathf.Clamp01(moveTime / moveDuration);

        Vector3 pos = BezierCubic(startPos, controlPoint1, controlPoint2, targetWorldPos, t);
        moveObject.transform.position = pos;

        // スケール縮小
        moveObject.transform.localScale = Vector3.Lerp(startScale, Vector3.one * minScale, t);

        if (t >= 1f)
        {
            Finish();
        }
    }

    /// <summary>
    /// UI RectTransform → 3D World
    /// </summary>
    private void UpdateTargetWorldPos()
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(
            null, // Overlayはnull
            targetUI[targetIndex].position
        );

        float z = mainCamera.WorldToScreenPoint(moveObject.transform.position).z;

        targetWorldPos = mainCamera.ScreenToWorldPoint(
            new Vector3(screenPos.x, screenPos.y, z)
        );
    }

    /// <summary>
    /// Canvas(0,0) → World
    /// </summary>
    private Vector3 GetUIZeroWorldPos()
    {
        RectTransform root = targetUI[targetIndex].root as RectTransform;

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(
            null,
            root.TransformPoint(Vector3.zero)
        );

        float z = mainCamera.WorldToScreenPoint(moveObject.transform.position).z;

        return mainCamera.ScreenToWorldPoint(
            new Vector3(screenPos.x, screenPos.y, z)
        );
    }

    public void StartMove()
    {
        if (isMoving) return;

        itemTracking.SetFollowing(false);
        moveObject.SetActive(true);

        startPos = moveObject.transform.position;
        moveObject.transform.localScale = startScale;

        UpdateTargetWorldPos();

        Vector3 uiZeroWorld = GetUIZeroWorldPos();

        controlPoint1 = Vector3.Lerp(startPos, uiZeroWorld, 0.5f) + Vector3.up * 2.5f;
        controlPoint2 = uiZeroWorld;

        moveTime = 0f;
        isMoving = true;
    }

    private void Finish()
    {
        isMoving = false;
        moveObject.transform.localScale = startScale;
        moveObject.SetActive(false);

        itemTracking.ResetItem(itemNumber);
        OnArrived?.Invoke();
    }

    private Vector3 BezierCubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        return u * u * u * p0 + 3 * u * u * t * p1 + 3 * u * t * t * p2 + t * t * t * p3;
    }
}