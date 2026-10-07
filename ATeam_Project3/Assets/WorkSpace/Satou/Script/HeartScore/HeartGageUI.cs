using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HeartGageUI : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI scoreText = null;
    [SerializeField]
    private TextMeshProUGUI[] addScoreText = null;
    [SerializeField]
    private ScoreMover scoreMover = null;
    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int succesScore = 0;
    [SerializeField]
    private int FailedScore = 0;
    [SerializeField]
    private float scoreDisplayDuration = 0.0f;

    private int addScoreIndex = 0;
    private int moveCount = 0;
    private float scoreDisplayTime = 0f;
    private bool isDisplayDuration = false;
    private bool isScoreMove = false;

    private void Start()
    {
        scoreText.text = score.ToString();
    }
    private void Update()
    {
        if(isDisplayDuration)
        {
            scoreDisplayTime += Time.deltaTime;
            if (scoreDisplayTime >= scoreDisplayDuration)
            {
                scoreText.text = score.ToString();
                scoreMover.AllFadeIn();
                addScoreIndex = scoreMover.GetMaxValue();
                isDisplayDuration = false;
                isScoreMove = false;
                moveCount = 0;
                scoreDisplayTime = 0f;
            }
        }
    }

    public void IsScoreSucces()
    {
        score += succesScore;
        scoreText.text = score.ToString();
        addScoreText[addScoreIndex].color = new Color(addScoreText[addScoreIndex].color.r, addScoreText[addScoreIndex].color.g, addScoreText[addScoreIndex].color.b, 1);
        addScoreText[addScoreIndex].text = "+" + succesScore.ToString();
        IndexPlus();
        isDisplayDuration = true;
        scoreDisplayTime = 0f;
    }

    public void IsScoreFailed()
    {
        score += FailedScore;
        scoreText.text = score.ToString();
        addScoreText[addScoreIndex].color = new Color(addScoreText[addScoreIndex].color.r, addScoreText[addScoreIndex].color.g, addScoreText[addScoreIndex].color.b, 1);
        addScoreText[addScoreIndex].text = "+" + FailedScore.ToString();
        IndexPlus();
        isDisplayDuration = true;
        scoreDisplayTime = 0f;
    }
    private void IndexPlus()
    {
        addScoreIndex++;
        moveCount++;
        if (moveCount >= addScoreText.Length)
        {
            isScoreMove = true;
        }
        if(addScoreIndex >= addScoreText.Length)
        {
            addScoreIndex = 0;
        }
        if (isScoreMove)
        {
            scoreMover.MoveStart();
        }
    }
    public int GetScore()
    {
        return score;
    }
}
