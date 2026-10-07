using UnityEngine;

public class HeartQTE : MonoBehaviour
{
    [SerializeField] private Transform[] player = null;
    [SerializeField] private GameObject[] heart = null;
    [SerializeField] private RectTransform[] heartTr = null;
    [SerializeField] private GameObject button = null;
    [SerializeField] private GameObject heartJudge = null;
    [SerializeField] private GameObject successHeart = null;
    [SerializeField] private GameObject heartLine = null;
    [SerializeField] private GameObject heartBase = null;
    [SerializeField] private GameObject heartCenter = null;
    [Header("ハートの目標位置のオフセット")]
    [SerializeField] private float offset = 0;
    [Header("ハートが目標位置に移動する時間")]
    [SerializeField] private float moveDuration = 0;
    [SerializeField] private float resetDelay = 0;

    [SerializeField] private ObjectSideShake objectSideShake = null;
    [SerializeField] private ScreenFade redOut = null;
    [SerializeField] private SEManager seManager = null;
    [SerializeField] private PlayerPullData pullData = null;

    private Vector2[] startPos = new Vector2[2];
    private Vector2[] targetPos = new Vector2[2];

    private bool playReset = false;
    private bool[] isMiss = new bool[2];

    private float[] moveTime = new float[2];
    private float countTime = 0;
    private float index = 0;
    private float playerDistance = 0;

    private void Start()
    {
        //初期位置と目標位置を設定
        startPos[0] = heartTr[0].anchoredPosition;
        startPos[1] = heartTr[1].anchoredPosition;
        targetPos[0] = Vector2.zero + Vector2.right * offset;
        targetPos[1] = Vector2.zero + Vector2.left * offset;

        //ハートとボタンを非表示にする
        heart[0].SetActive(false);
        heart[1].SetActive(false);
        button.SetActive(false);
        heartJudge.SetActive(false);
        successHeart.SetActive(false);
        heartLine.SetActive(false);
        heartBase.SetActive(false);
        heartCenter.SetActive(false);
    }

    private void Update()
    {
        //プレイヤー同士の距離を取得
        playerDistance = Vector3.Distance(player[0].position, player[1].position);

        //時間のスケールに応じてインデックスを設定
        if (Time.timeScale != 1)
        {
            index = 1.0f / Time.timeScale;
        }
        else
        {
            index = 1.0f;
        }

        //リセット中の処理
        if (playReset)
        {
            countTime += Time.deltaTime * index;
            if (countTime >= resetDelay)
            {
                countTime = 0;

                playReset = false;
                isMiss[0] = false;
                isMiss[1] = false;
                moveTime[0] = 0;
                moveTime[1] = 0;

                heartTr[0].anchoredPosition = startPos[0];
                heartTr[1].anchoredPosition = startPos[1];

                heart[0].SetActive(false);
                heart[1].SetActive(false);
                button.SetActive(false);
                heartJudge.SetActive(false);
                successHeart.SetActive(false);
                heartLine.SetActive(false);
                heartBase.SetActive(false);
                heartCenter.SetActive(false);
            }
        }
    }

    public void MoveHeart(int number)//ハートを動かす処理
    {
        if (!heart[number].activeSelf)
        {
            heart[number].SetActive(true);
        }

        if (!button.activeSelf)
        {
            button.SetActive(true);
            heartJudge.SetActive(true);
            heartLine.SetActive(true);
            heartBase.SetActive(true);
            heartCenter.SetActive(true);
        }

        //プレイヤー同士の距離が一定以下の場合にハートを動かす
        if (playerDistance <= pullData.GetCanPullDistance())
        {
            moveTime[number] += Time.deltaTime * index;

            float t = moveTime[number] / moveDuration;

            heartTr[number].anchoredPosition =
                Vector2.Lerp(startPos[number], targetPos[number], t);
        }
    }

    public void Miss(int number)//ミスの処理
    {
        if (isMiss[number])
        {
            return;
        }

        isMiss[number] = true;
        seManager.PlayPlayerCatchFailSE(); //キャッチ失敗SEを再生する
        objectSideShake.Shake();
        redOut.SetFadeImage();
        redOut.StartFadeOut();
    }

    public void ResetQte()//リセットの処理
    {
        if(playReset)
        {
            return;
        }

        playReset = true;
    }

    public void Success()//成功の処理
    {
        heart[0].SetActive(false);
        heart[1].SetActive(false);
        successHeart.SetActive(true);
    }

    public float GetDistance(int number)//ハートと目標位置の距離を取得する処理
    {
        return Mathf.Abs(heartTr[number].anchoredPosition.x);
    }
}