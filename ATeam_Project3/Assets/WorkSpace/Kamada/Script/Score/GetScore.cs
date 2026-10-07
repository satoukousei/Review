using UnityEngine;

public class GetScore : MonoBehaviour
{
    [SerializeField]
    private GameObject[] UI;
    [SerializeField]
    private AudioSource se;
    [SerializeField]
    private AudioSource[] bgm;
    [SerializeField]
    private float drumTime = 8.0f;
    [SerializeField]
    private int[] scoreRank;

    private GameObject score = null;
    private int number = 0;
    private bool isDrum = true;
    private bool isPlay = false;
    private void Awake()
    {
        for (int i = 0; i < UI.Length; i++)
        {
            UI[i].SetActive(false);
        }
    }
    private void Start()
    {
        score = GameObject.Find("SaveScore");
        int lastScore;
        if (score != null)
        {
            SaveScore saveScore = score.GetComponent<SaveScore>();
            lastScore = saveScore.GetScore();
        }
        else
        {
            lastScore = 0;
        }

        Destroy(score);

        if (lastScore <= scoreRank[0])
        {
            number = 0;
        }
        else if(lastScore > scoreRank[0] && lastScore <= scoreRank[1])
        {
            number = 1;
        }
        else if(lastScore > scoreRank[1] && lastScore <= scoreRank[2])
        {
            number = 2;
        }
        else if(lastScore > scoreRank[2])
        {
            number = 3;
        }

        se.Play();
        isDrum = true;
    }

    private void Update()
    {
        if (isDrum)
        {
            if(se.time >= drumTime)
            {
                isDrum = false;
            }
        }
        else
        {
            if (!isPlay)
            {
                ActiveUI();
                isPlay = true;
            }
        }
    }

    private void ActiveUI()
    {
        UI[number].SetActive(true);
        UI[4].SetActive(true);
        bgm[number].Play();
    }

    public bool GetIsPlay()
    {
        return isPlay;
    }
}
