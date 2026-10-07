using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SimpleStretchRope : MonoBehaviour
{
    [Header("設定")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [Header("ロープパラメータ")]
    [SerializeField, Tooltip("通常の長さ")]
    private float baseLength = 2.0f;
    [SerializeField, Tooltip("どれくらい伸びるか")]
    private float maxStretch = 1.0f;
    [SerializeField, Tooltip("縮むスピード")]
    private float shrinkSpeed = 5.0f;
    [SerializeField, Tooltip("伸びるスピード")]
    private float stretchSpeed = 3.0f;

    [Header("見た目")]
    [SerializeField] private int smoothSegment = 20;
    [SerializeField] private float ropeWidth = 0.1f;
    [SerializeField] private Color ropeColor = Color.yellow;

    private float currentLength;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Mathf.Max(2, smoothSegment);
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        lineRenderer.startColor = ropeColor;
        lineRenderer.endColor = ropeColor;

        currentLength = baseLength;
    }

    void Update()
    {
        float dist = Vector3.Distance(startPoint.position, endPoint.position);
        float maxLength = baseLength + maxStretch;

        // ---- ロープ伸縮処理 ----
        if (dist > currentLength)
        {
            currentLength = Mathf.MoveTowards(
                currentLength,
                Mathf.Min(dist, maxLength),
                Time.deltaTime * stretchSpeed
            );
        }
        else
        {
            currentLength = Mathf.MoveTowards(
                currentLength,
                dist,
                Time.deltaTime * shrinkSpeed
            );
        }

        currentLength = Mathf.Clamp(currentLength, baseLength, maxLength);

        if (dist > maxLength)//ロープの長さが最大になったらお互いを引っ張る
        {
            Vector3 midPoint = Vector3.Lerp(startPoint.position, endPoint.position, 0.5f);

            Vector3 dirStart = (startPoint.position - midPoint).normalized;
            Vector3 dirEnd = (endPoint.position - midPoint).normalized;

            startPoint.position = midPoint + dirStart * (maxLength * 0.5f);
            endPoint.position = midPoint + dirEnd * (maxLength * 0.5f);
        }

        UpdateRope();
    }

    void UpdateRope()
    {
        Vector3 start = startPoint.position;
        Vector3 end = endPoint.position;

        float dist = Vector3.Distance(start, end);
        float ropeRatio = Mathf.Clamp01(dist / currentLength);

        for (int i = 0; i < smoothSegment; i++)
        {
            float t = (float)i / (smoothSegment - 1);
            Vector3 pos = Vector3.Lerp(start, end, t * ropeRatio);
            lineRenderer.SetPosition(i, pos);
        }
    }
}
