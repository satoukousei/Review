using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// RopeTestReal_LineRenderer
/// ConfigurableJoint を使ったリアルなロープ挙動 + LineRenderer で見た目を滑らかにする。
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class RopeTestReal_LineRenderer : MonoBehaviour
{
    [Header("基本設定")]
    [SerializeField] private Transform startPoint;      // ロープの始点
    [SerializeField] private Transform endPoint;        // ロープの終点（オプション）
    [SerializeField] private int segmentCount = 15;     // セグメント（節）の数
    [SerializeField] private float segmentLength = 0.3f;// 各節の距離
    [SerializeField] private GameObject segmentPrefab;  // Ropeセグメント用のプレハブ
    [SerializeField] private bool attachEnd = true;     // 終点を固定するか

    [Header("物理パラメータ")]
    [SerializeField] private float segmentMass = 0.1f;  // 各節の質量
    [SerializeField] private float spring = 50f;        // 張力（強いほどピンと張る）
    [SerializeField] private float damper = 2f;         // 減衰（大きいほど揺れにくい）
    [SerializeField] private float angularLimit = 30f;  // 曲がりの許容角度（°）

    [Header("見た目設定")]
    [SerializeField] private float ropeWidth = 0.05f;   // ロープの太さ
    [SerializeField] private Color ropeColor = Color.yellow; // ロープの色

    private Rigidbody[] segmentBodies;
    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer 初期設定
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = ropeColor;
        lineRenderer.endColor = ropeColor;
        lineRenderer.numCapVertices = 4; // 両端を丸くする

        if (segmentPrefab == null)
        {
            Debug.LogError("segmentPrefab が設定されていません。");
            return;
        }
        CreateRope();
    }

    void Update()
    {
        // Rキーで再生成（デバッグ用）
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ClearRope();
            CreateRope();
        }

        // LineRenderer更新
        UpdateLineRenderer();
    }

    /// <summary>
    /// 既存のロープを削除
    /// </summary>
    void ClearRope()
    {
        if (segmentBodies == null) return;
        foreach (var rb in segmentBodies)
        {
            if (rb != null) Destroy(rb.gameObject);
        }
    }

    /// <summary>
    /// ロープの物理構造を作成
    /// </summary>
    void CreateRope()
    {
        segmentBodies = new Rigidbody[segmentCount];

        Vector3 startPos = startPoint.position;
        Vector3 endPos = endPoint != null
            ? endPoint.position
            : startPos + Vector3.down * segmentLength * (segmentCount - 1);

        Vector3 dir = (endPos - startPos).normalized;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 pos = Vector3.Lerp(startPos, endPos, (float)i / (segmentCount - 1));
            GameObject segment = Instantiate(segmentPrefab, pos, Quaternion.identity, transform);
            segment.name = $"RopeSegment_{i}";

            Rigidbody rb = segment.GetComponent<Rigidbody>();
            if (rb == null) rb = segment.AddComponent<Rigidbody>();
            rb.mass = segmentMass;
            rb.linearDamping = 0.1f;
            rb.angularDamping = 0.05f;
            rb.useGravity = true;

            segmentBodies[i] = rb;

            if (i == 0)
            {
                // 始点
                ConfigurableJoint joint = segment.AddComponent<ConfigurableJoint>();
                joint.connectedBody = startPoint.GetComponent<Rigidbody>();
                joint.autoConfigureConnectedAnchor = false;
                joint.anchor = Vector3.zero;
                joint.connectedAnchor = Vector3.zero;
                joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Locked;
            }
            else
            {
                // 中間セグメント
                ConfigurableJoint joint = segment.AddComponent<ConfigurableJoint>();
                joint.connectedBody = segmentBodies[i - 1];
                joint.autoConfigureConnectedAnchor = false;
                joint.anchor = Vector3.zero;
                joint.connectedAnchor = new Vector3(0, -segmentLength, 0);

                joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = joint.angularYMotion = joint.angularZMotion = ConfigurableJointMotion.Limited;

                SoftJointLimit limit = new SoftJointLimit { limit = segmentLength };
                joint.linearLimit = limit;

                SoftJointLimitSpring limitSpring = new SoftJointLimitSpring
                {
                    spring = spring,
                    damper = damper
                };
                joint.linearLimitSpring = limitSpring;

                SoftJointLimit angular = new SoftJointLimit { limit = angularLimit };
                joint.lowAngularXLimit = angular;
                joint.highAngularXLimit = angular;
                joint.angularYLimit = angular;
                joint.angularZLimit = angular;
            }
        }

        // 終端処理
        if (attachEnd && endPoint != null)
        {
            FixedJoint endJoint = segmentBodies[segmentCount - 1].gameObject.AddComponent<FixedJoint>();
            Rigidbody endRb = endPoint.GetComponent<Rigidbody>();
            if (endRb == null)
            {
                endRb = endPoint.gameObject.AddComponent<Rigidbody>();
                endRb.isKinematic = true;
            }
            endJoint.connectedBody = endRb;
        }

        // LineRenderer用に点の数を設定
        lineRenderer.positionCount = segmentCount;
    }

    /// <summary>
    /// LineRendererの描画更新
    /// </summary>
    void UpdateLineRenderer()
    {
        if (segmentBodies == null || lineRenderer == null) return;

        for (int i = 0; i < segmentBodies.Length; i++)
        {
            if (segmentBodies[i] != null)
            {
                lineRenderer.SetPosition(i, segmentBodies[i].position);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (segmentBodies == null) return;
        Gizmos.color = Color.yellow;

        for (int i = 0; i < segmentBodies.Length - 1; i++)
        {
            if (segmentBodies[i] && segmentBodies[i + 1])
            {
                Gizmos.DrawLine(segmentBodies[i].position, segmentBodies[i + 1].position);
            }
        }
    }
}