using Unity.VisualScripting;
using UnityEngine;
public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform player1 = null;
    [SerializeField] private PlayerPull pull1 = null;
    [SerializeField] private Transform player2 = null;
    [SerializeField] private PlayerPull pull2 = null;

    [SerializeField,Header("カメラ位置")] private Vector3 offset = new Vector3(0, 2, -5);
    [SerializeField, Header("ズーム速度")] private float zoomSpeed = 5f;
    [SerializeField, Header("成功時のカメラが戻る速度")] private float successReturnSpeed = 3f;
    [SerializeField, Header("失敗時のカメラが戻る速度")] private float misReturnSpeed = 1f;
    [SerializeField, Header("成功時のカメラの角度が戻る速度")] private float successReturnRotateSpeed = 3f;
    [SerializeField, Header("失敗時のカメラの角度が戻る速度")] private float missReturnRotateSpeed = 1f;
    [SerializeField, Header("成功時のカメラ固定時間")] private float successDelay = 1f;
    [SerializeField, Header("失敗時のカメラ固定時間")] private float missdelay = 1f;

    [SerializeField] private PlayerPullData pullData = null;

    private Vector3 originalPos = Vector3.zero;
    private float distance = 0;
    private Quaternion originalRot;

    private bool isZoom = false;
    private bool isPlayer1 = false;
    private bool isReturn = false;
    private bool isSuccess = false;

    private float delayTimer = 0f;
    private float index = 1;

    private void Awake()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    private void Update()
    {
        if (Time.timeScale != 1)
        {
            index = 1 / Time.timeScale;
        }
        else
        {
            index = 1;
        }

        distance = Vector3.Distance(player1.position, player2.position);

        StartZoom();

        if (isZoom)
        {
            ZoomCamera();
        }

        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime * index;

            if (delayTimer <= 0)
            {
                isReturn = true;
            }
        }

        if (isReturn)
        {
            ResetCameraPosition();
        }
    }

    private void StartZoom()
    {
        if (!isZoom)
        {
            if (pull1.GetIsPulling())
            {
                isZoom = true;
                isPlayer1 = true;
            }

            if (pull2.GetIsPulling())
            {
                isZoom = true;
                isPlayer1 = false;
            }
        }
    }
    private void ZoomCamera()
    {
        Transform target;

        if (isPlayer1)
        {
            target = player1;
        }
        else
        {
            target = player2;
        }

        if (distance <= pullData.GetCanPullDistance())
        {
            Vector3 targetPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * zoomSpeed * index);
        }
       
        Vector3 lookPos = (player1.position + player2.position) / 2;
        transform.LookAt(new Vector3(lookPos.x, lookPos.y + 0.5f, lookPos.z));

        PlayerPull pull;

        if (isPlayer1)
        {
            pull = pull1;
        }
        else
        {
            pull = pull2;
        }

        if (!pull.GetIsPulling())
        {
            isZoom = false;

            if (pull.GetIsCatch())
            {
                delayTimer = successDelay;
                isSuccess = true;
            }
            else
            {
                delayTimer = missdelay;
                isSuccess = false;
            }
        }
    }

    private void ResetCameraPosition()
    {
        float moveSpeed,rotateSpeed;
        if (isSuccess)
        {
            moveSpeed = successReturnSpeed;
            rotateSpeed = successReturnRotateSpeed;
        }
        else
        {
            moveSpeed = misReturnSpeed;
            rotateSpeed = missReturnRotateSpeed;
        }

        transform.position = Vector3.Lerp(transform.position, originalPos, Time.deltaTime * moveSpeed * index);
        transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, Time.deltaTime * rotateSpeed * index);
        //transform.rotation = originalRot;

        if (Vector3.Distance(transform.position, originalPos) < 0.05f || isZoom)
        {
            isReturn = false;
            isSuccess = false;
        }
    }
}