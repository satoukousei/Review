using UnityEngine;

public class Knockback : MonoBehaviour
{
    [Header("ノックバック設定")]
    [SerializeField]
    private float knockbackPower = 10f;
    [Header("判定設定")]
    [SerializeField]
    private string[] aoeTags = new string[0];
    [SerializeField]
    private PlayerPull pull = null;
    [SerializeField]
    private Rigidbody rb = null;

    private bool isKnockback = false;
    private bool isPulling = false;

    private void Update()
    {
        if (pull != null)
        {
            isPulling = pull.GetIsPulling() || pull.GetIsBeingPulled();
        }
    }

    //Trigger判定
    private void OnTriggerEnter(Collider other)
    {
        TryApplyKnockback(other.gameObject);
    }

    //物理衝突判定
    private void OnCollisionEnter(Collision collision)
    {
        TryApplyKnockback(collision.gameObject);
    }

    private void TryApplyKnockback(GameObject other)
    {
        if (other == null || other == gameObject) return;
        if (isPulling) return;

        if (!IsMatchTag(other)) return;

        ApplyKnockback(other.transform, knockbackPower);
    }

    //タグ一致判定
    private bool IsMatchTag(GameObject other)
    {
        if (aoeTags == null || aoeTags.Length == 0) return false;

        foreach (var tag in aoeTags)
        {
            if (!string.IsNullOrEmpty(tag) && other.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }

    // ノックバック処理（Y軸無視）
    public void ApplyKnockback(Transform aoeTransform, float power)
    {
        if (rb == null || aoeTransform == null) return;

        Vector3 dir = transform.position - aoeTransform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude <= 0f) return;

        dir.Normalize();

        float currentY = rb.linearVelocity.y;
        Vector3 newVelocity = new Vector3(dir.x * power, currentY, dir.z * power);

        rb.linearVelocity = newVelocity;

        transform.rotation = Quaternion.LookRotation(-dir);

        isKnockback = true;
    }

    public void ResetKnockback()
    {
        isKnockback = false;
    }

    public bool GetIsKnockBack()
    {
        return isKnockback;
    }

    public void SetKnockBack()
    {
        isKnockback = false;
    }
}