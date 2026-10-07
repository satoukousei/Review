using UnityEngine;

public class Rope : MonoBehaviour
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
    [SerializeField]
    private GameObject ropeObject = null;

    private float currentLength;

    void Start()
    {
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
        Vector3 start = startPoint.position + new Vector3(0, startPoint.localScale.y / 4, 0);
        Vector3 end = endPoint.position + new Vector3(0, endPoint.localScale.y / 4, 0);

        float dist = Vector3.Distance(start, end);
        Vector3 dir = (start - end).normalized;
        Vector3 pos = (start + end) / 2;
        Vector3 size = ropeObject.transform.localScale;

        size.z = dist;
        ropeObject.transform.localScale = size;
        ropeObject.transform.rotation = Quaternion.LookRotation(dir);
        ropeObject.transform.position = pos;
    }
}
