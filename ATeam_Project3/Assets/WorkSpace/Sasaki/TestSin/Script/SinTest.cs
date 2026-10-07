using UnityEngine;

public class SinTest : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 1.0f; // 距離
    [SerializeField]
    private float speed = 1.0f;     // 速度
    [SerializeField]
    private Transform centerPoint; // 中心点
    [SerializeField]
    private float startAngle = 0.0f; // 開始角度
    [SerializeField]
    private bool isMove = true;      // 移動フラグ

    private bool  isRotate    = true;
    private float angle       = 0;
    private float targetAngle = 0;
    private float moveDir     = 1;

    // Update is called once per frame
    private void Update()
    {
        if (isRotate)
        {
            if (isMove)
            {
                Move();
            }
            if (!isMove)
            {
                angle += speed * Time.deltaTime * moveDir;
                Move();
            }
            if(moveDir < 0)
            {
                if (angle <= targetAngle)
                {
                    isRotate = false;
                }
                return;
            }
            else
            {
                if (angle >= targetAngle)
                {
                    isRotate = false;
                }
            }
        }
    }
    private void Move()
    {
        this.transform.position = new Vector3(centerPoint.position.x + amplitude * Mathf.Sin(angle),
                                      this.transform.position.y,
                                      centerPoint.position.z + amplitude * Mathf.Cos(angle));
    }

    public void OldAngleSet(Transform center)
    {
        angle = (center.eulerAngles.y - 180.0f) * Mathf.Deg2Rad;
    }
    public void OldTargetAngleSet(Transform center)
    {
        targetAngle = (center.eulerAngles.y - 180.0f) * Mathf.Deg2Rad;
    }

    public void DirSet(int dir)
    { 
        moveDir = dir > 0 ? 1 :-1;
    }
}
