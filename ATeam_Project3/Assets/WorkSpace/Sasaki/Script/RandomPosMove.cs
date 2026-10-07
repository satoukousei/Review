using UnityEngine;

public class RandomPosMove : MonoBehaviour
{
    [SerializeField, Header("ˆÚ“®‚³‚¹‚½‚¢UI")]
    private RectTransform rectTransform = null;
    [SerializeField, Header("ˆÚ“®”ÍˆÍ")]
    private Vector2 moveRange       = new Vector2(5f, 5f);
    [SerializeField, Header("ˆÚ“®”ÍˆÍ")]
    private Vector2 lowerLimit      = new Vector2(2f, 2f);
    [SerializeField, Header("ˆÚ“®‰ñ”")]
    private int     moveCount       = 10;
    [SerializeField, Header("ˆÚ“®ŽžŠÔ")]
    private int     lowerLimitCount = 10;
    [SerializeField, Header("‰ñ”/ŽžŠÔ")]
    private bool    isCount         = false;
    [SerializeField, Header("ˆÚ“®ŠÔŠu")]
    private float   moveInterval    = 1f;

    private bool    isMoving     = false;
    private float   intervalTime = 0f;
    private float   limitedTime  = 0f;
    private int     count        = 0;
    private Vector2 oldPosition  = Vector3.zero;

    private void Start()
    {
        count = moveCount;
    }

    private void Update()
    {
        if(isMoving)
        {
            intervalTime += Time.deltaTime;
            if(intervalTime >= moveInterval)
            {
                MovePos();
                if (isCount)
                {
                    moveCount--;
                }
                else
                {
                    limitedTime += intervalTime; 
                }
                intervalTime = 0f;
                if(moveCount <= 0 || limitedTime >= lowerLimitCount)
                {
                    moveCount = count;
                    limitedTime = 0;
                    rectTransform.anchoredPosition = oldPosition;
                    isMoving = false;
                }
            }
        }
    }

    private void MovePos()
    {
        float a = Random.value >= 0.5f ? 1 : -1;
        float b = Random.value >= 0.5f ? 1 : -1;

        rectTransform.anchoredPosition = oldPosition +
            new Vector2((lowerLimit.x + (moveRange.x * Random.value))* a,
                        (lowerLimit.y + (moveRange.y * Random.value)) * b);
    }

    public void StartMove(Vector2 pos)
    {
        if(!isMoving)
        {
            isMoving = true;
            oldPosition = pos;
            MovePos();
        }
    }
}
