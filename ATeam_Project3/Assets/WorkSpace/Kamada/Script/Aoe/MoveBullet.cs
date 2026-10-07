using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    private Vector3 dir = Vector3.zero;
    private float moveSpeed = 0;
    private bool isPlay = false;

    private void FixedUpdate()
    {
        if (isPlay)
        {
            Vector3 pos = rb.position;
            Vector3 newPos = pos + moveSpeed * Time.deltaTime * dir;
            rb.MovePosition(newPos);
        }
    }

    public void SetMove(float speed,Vector3 direction)
    {
        moveSpeed = speed;
        dir = direction.normalized;
    }
    public void PlayMove()
    {
        isPlay = true;
    }
}
