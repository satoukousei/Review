using UnityEngine;

public class ShadowRay : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Renderer shadowRenderer;
    [SerializeField] private float rayDistance = 100f;

    private float height;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(player.position, Vector3.down, out hit, rayDistance))
        {
            // タグチェック
            if (hit.collider.CompareTag("Ground"))
            {
                float height = player.position.y - hit.point.y;
                if (height <= 10f)
                {
                    transform.position = hit.point + Vector3.up * 0.01f;
                    shadowRenderer.enabled = true;
                }
            }
            else
            {
                shadowRenderer.enabled = false;
            }
        }
        else
        {
            shadowRenderer.enabled = false;
        }
        Vector3 scale = transform.localScale;
        scale.y = 0.1f;
        transform.localScale = scale;

    }
}
