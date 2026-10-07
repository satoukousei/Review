using UnityEngine;

public class TitleMove : MonoBehaviour
{
    [SerializeField] private RectTransform titleImage = null;
    [SerializeField] private RectTransform target = null;
    [SerializeField] private GameObject backGround = null;
    [SerializeField] private float waitTime = 0;
    [SerializeField] private float moveSpeed = 0;
    [SerializeField] private float minScale = 0f;

    private bool isMove = false;
    private bool isStop = false;
    private float countTime = 0;
    private float startDistance = 0;
    private Vector3 startScale = Vector3.zero;

    private void Start()
    {
        countTime = 0;
        isMove = false;
        isStop = false;
        startDistance = Vector3.Distance(target.position, titleImage.position);
        startScale = titleImage.localScale;
        backGround.SetActive(true);
    }
    private void Update()
    {
        if (isStop)
        {
            return;
        }
        TimeCount();
    }
    private void FixedUpdate()
    {
        if (isStop)
        {
            return;
        }
        Move();
    }
    private void TimeCount()
    {
        if (!isMove)
        {
            countTime += Time.deltaTime;

            if (countTime >= waitTime)
            {
                countTime = 0;
                isMove = true;
            }
        }
    }
    private void Move()
    {
        if (isMove)
        {
            Vector3 dir = target.position - titleImage.position;
            dir.Normalize();
            Vector3 newPos = titleImage.position + moveSpeed * Time.deltaTime * dir;
            titleImage.position = newPos;

            float currentDistance = Vector3.Distance(target.position, titleImage.position);

            float t = currentDistance / startDistance;
            t = Mathf.Clamp01(t);

            float scaleValue = Mathf.Lerp(minScale, 1f, t);
            titleImage.localScale = startScale * scaleValue;

            if (currentDistance <= 2.5f)
            {
                backGround.SetActive(false);
                isStop = true;
            }
        }
    }
}
