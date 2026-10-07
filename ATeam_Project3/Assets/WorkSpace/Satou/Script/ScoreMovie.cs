using TMPro;
using UnityEngine;
using UnityEngine.Video;
public class ScoreMovie : MonoBehaviour
{
    [SerializeField]
    private int[] scoreRank;
    [SerializeField]
    private VideoClip[] videoClip;
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private RectTransform textTr;
    [SerializeField]
    private float[] posX;
    [SerializeField]
    private float scoreView = 0;
    [SerializeField]
    private SEManager se;
    [SerializeField]
    private GameObject sceneChangetext;

    private GameObject score = null;
    private int number = 0;
    private int lastScore = 0;

    private bool isPlay = false;

    private void Start()
    {
        text.gameObject.SetActive(false);
        sceneChangetext.SetActive(false);

        score = GameObject.Find("SaveScore");

        if (score != null)
        {
            SaveScore saveScore = score.GetComponent<SaveScore>();
            lastScore = saveScore.GetScore();
        }
        else
        {
            lastScore = 3000;
        }

        Destroy(score);
        if (lastScore <= scoreRank[0])
        {
            number = 0;

        }
        else if (lastScore > scoreRank[0] && lastScore <= scoreRank[1])
        {
            number = 1;

        }
        else if (lastScore > scoreRank[1] && lastScore <= scoreRank[2])
        {
            number = 2;

        }
        else if (lastScore > scoreRank[2])
        {
            number = 3;

        }

        Vector3 pos = textTr.localPosition;
        if (number == 0)
        {
            pos.x = posX[0];
        }
        else
        {
            pos.x = posX[1];
        }
        textTr.localPosition = pos;

        videoPlayer.clip = videoClip[number];
        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = false;
        videoPlayer.skipOnDrop = false;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;

        text.SetText("");
    }
    private void OnPrepared(VideoPlayer vp)
    {
        vp.time = 0.1f;
        vp.Play();
    }
    private void Update()
    {
        if (!isPlay && videoPlayer.time >= scoreView)
        {
            isPlay = true;
            SetBGM(number);
            text.gameObject.SetActive(true);
            sceneChangetext.SetActive(true);
            text.SetText(lastScore.ToString());
        }
    }

    private void SetBGM(int num)
    {
        switch (num)
        {
            case 0:
                {
                    se.PlayResultRankSE("C");
                    break;
                }
            case 1:
                {
                    se.PlayResultRankSE("B");
                    break;
                }
            case 2:
                {
                    se.PlayResultRankSE("A");
                    break;
                }
            case 3:
                {
                    se.PlayResultRankSE("S");
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public bool GetIsPlay()
    {
        return isPlay;
    }
}