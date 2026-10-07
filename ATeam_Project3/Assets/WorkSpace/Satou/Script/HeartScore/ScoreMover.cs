using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreMover : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] scoreTextPos = null;
    [SerializeField]
    private TextFade[] scoreFade = null;
    [SerializeField]
    private TextMeshProUGUI[] scoreText = null;

    [SerializeField]
    private float moveSpeedY = 0.0f;
    [SerializeField]
    private float moveLimit = 0.0f;
    [SerializeField]
    private int maxValue = 0;

    private float moveCount = 0.0f;

    private bool isStart = false;
    private bool isFade = false;
    private Vector2 finishRTr;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(isStart)
        {           
            if (!isFade)
            {
                int value = maxValue - 1 < 0 ? scoreFade.Length - 1 : maxValue - 1;
                scoreFade[value].FadeOut();
                scoreFade[maxValue].FadeIn();
                finishRTr = scoreTextPos[value].anchoredPosition;
                isFade = true;
            }
            
            MovescoreTextPos();
        }
    }

    private void MovescoreTextPos()
    {
        for(int i = 0; i < scoreTextPos.Length; i++)
        {
            scoreTextPos[i].anchoredPosition += new Vector2(0, moveSpeedY * Time.deltaTime);
        }
        moveCount += moveSpeedY * Time.deltaTime;
        if(moveCount >= moveLimit)
        {
            isStart = false;
            for(int i = 0; i < scoreTextPos.Length; i++)
            {
                scoreTextPos[i].anchoredPosition -= new Vector2(0, moveCount - moveLimit);
            }
            moveCount = 0.0f;
            scoreFade[maxValue].StopFade();
            scoreText[maxValue].text = "";
            scoreTextPos[maxValue].anchoredPosition = finishRTr;
            maxValue++;
             if(maxValue >= scoreFade.Length)
            {
                maxValue = 0;
            }
             isFade = false;
        }
    }
    public void MoveStart()
    {
        isStart = true;
    }

    public void AllFadeIn()
    {
        for(int i = 0; i < scoreFade.Length; i++)
        {
            scoreFade[i].FadeIn();
        }
    }
    public int GetMaxValue()
    {
        return maxValue;
    }
}