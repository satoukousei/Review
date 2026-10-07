using UnityEngine;

public class SaveScore : MonoBehaviour
{
    [SerializeField]
    private FadeCreate fade = null;
    [SerializeField]
    private HeartGageUI heartGageUI = null;
    [SerializeField]
    private PhaseManager phaseManager = null;

    private int score = 0;
    private bool isGameOver = false;

    private void Update()
    {
        if (phaseManager.GetIsEnd())
        {
            score = heartGageUI.GetScore();
            if (!isGameOver)
            {
                fade.FadeOutStart();
                isGameOver = true;
            }
        }
    }

    public int GetScore()
    {
        return score;
    }
}
