using UnityEngine;

public class HeartMove : MonoBehaviour
{
    [SerializeField]
    private RectTransform heartPosition;
    [SerializeField]
    private RectTransform startPosition;
    [SerializeField]
    private RectTransform targetPosition;
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private float removeSpeed = 0.0f;
    [SerializeField]
    private float rangeDistance = 0.0f;

    private bool isStart = false;
    private bool isEnd = false;

    private void Update()
    {
        if (isStart)
        {
            Move(startPosition, targetPosition, moveSpeed);
        }

        if (isEnd)
        {
            Move(targetPosition, startPosition, removeSpeed);
        }
    }

    private void Move(RectTransform startPos, RectTransform targetPos, float speed)
    {
        Vector2 direction = (targetPos.anchoredPosition - startPos.anchoredPosition).normalized;
        heartPosition.anchoredPosition += direction * speed * Time.deltaTime;
        float distance = Vector2.Distance(heartPosition.anchoredPosition, targetPos.anchoredPosition);
        if (distance <= rangeDistance)
        {
            heartPosition.anchoredPosition = targetPos.anchoredPosition;
            if(heartPosition.anchoredPosition == targetPosition.anchoredPosition)
            {
                isStart = false;
            }
            if (heartPosition.anchoredPosition == startPosition.anchoredPosition)
            {
                isEnd = false;
            }
        }
    }

    public void MoveStart()
    {
        isStart = true;
        isEnd = false;
    }

    public void RemoveStart()
    {
        isStart = false;
        isEnd = true;
    }
}
