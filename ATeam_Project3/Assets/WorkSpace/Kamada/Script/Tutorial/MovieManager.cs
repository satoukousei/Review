using UnityEngine;
using UnityEngine.Video;

public class MovieManager : MonoBehaviour
{
    [Header("再生する動画")]
    [SerializeField] private VideoPlayer videoPlayer = null;
    [Header("チュートリアル用勇者の入力情報")]
    [SerializeField] private TutorialInput heroInput = null;
    [Header("チュートリアル用魔王の娘の入力情報")]
    [SerializeField] private TutorialInput womenInput = null;
    [Header("フェード")]
    [SerializeField] private FadeCreate fade = null;
    [Header("動画を止めるポイントの情報")]
    [SerializeField] private float[] stopTime = new float[0];
    [Header("動画を止めたポイントの条件")]
    [Header("0 → プレイヤーの両方が押す")]
    [Header("1 → プレイヤーの片方が押す")]
    [Header("2 → プレイヤー1(勇者)が押す")]
    [Header("3 → プレイヤー2(魔王の娘)が押す")]
    [SerializeField] private int[] stopInformation = new int[0];
    [Header("勇者のボタン演出")]
    [SerializeField] private ButtonAnimation heroButtonAnimation = null;
    [SerializeField] private ButtonPressAnimation heroButtonPressAnimation = null;
    [Header("魔王の娘のボタン演出")]
    [SerializeField] private ButtonAnimation womenButtonAnimation = null;
    [SerializeField] private ButtonPressAnimation womenButtonPressAnimation = null;
    [Header("フェードを始めるまでの時間")]
    [SerializeField] private float delayTime = 0;
    [Header("次に進んだ時入力を許可するまでの時間")]
    [SerializeField] private float inputDelayTime = 0;

    private int stopCount = 0;
    private float countDelay = 0;
    private float inputDelay = 0;

    private bool isPause = false;
    private bool isEnd = false;
    private bool isSuccess = false;
    private bool isHeroPushButton = false;
    private bool isWomenPushButton = false;
    private bool isStop = false;
    private bool isInputLocked = false;

    private void Start()
    {
        countDelay = 0;
        videoPlayer.playOnAwake = false;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
    }
    private void OnPrepared(VideoPlayer vp)
    {
        vp.time = 0.1f;
        vp.Play();
    }

    private void Update()
    {
        //動画の止める時間を確認する処理
        CheckPauseTime();

        //入力受付までの時間をカウントする処理
        CountInputDelay();

        //動画再生が止まっていて入力受付が可能なら
        if (isPause && !isInputLocked)
        {
            //条件を達成したら動画再生を再開する処理
            TryResumePlayback(stopInformation[stopCount]);
        }
    }
    /// <summary>
    /// ポーズさせる時間かどうかを判定する
    /// </summary>
    private void CheckPauseTime()
    {
        if (stopCount < stopTime.Length)
        {
            if (videoPlayer.time >= stopTime[stopCount] && !isPause)
            {
                Pause();
            }
        }
        else
        {
            if (!videoPlayer.isPlaying)
            {
                isStop = true;
                countDelay += Time.deltaTime;

                if (!isEnd && countDelay >= delayTime)
                {
                    fade.FadeOutStart();
                    isEnd = true;
                    isStop = false;
                }
            }
        }
    }
    /// <summary>
    /// 次の入力を受け付けるまでの時間をカウントする処理
    /// </summary>
    private void CountInputDelay()
    {
        if (isInputLocked)
        {
            inputDelay += Time.deltaTime;

            if (inputDelay >= inputDelayTime)
            {
                inputDelay = 0;
                isInputLocked = false;
            }
        }
    }
    /// <summary>
    /// 動画再生を一時停止する処理
    /// </summary>
    private void Pause()
    {
        videoPlayer.Pause();
        isInputLocked = true;
        isPause = true;
        isSuccess = false;
    }
    /// <summary>
    /// 動画再生を再開する処理
    /// </summary>
    private void Play()
    {
        stopCount++;
        isPause = false;
        isHeroPushButton = false;
        isWomenPushButton = false;
        isSuccess = true;

        videoPlayer.Play();
    }
    /// <summary>
    /// 条件を指定してその条件が満たされるまで待つ処理
    /// </summary>
    /// <param name="index">どの条件かを決める変数</param>
    private void TryResumePlayback(int index)
    {
        switch (index)
        {
            case 0://プレイヤー両方がボタンを押したら
                {
                    if (!isHeroPushButton)
                    {
                        heroButtonAnimation.Play();
                    }

                    if (!isWomenPushButton)
                    {
                        womenButtonAnimation.Play();
                    }

                    if (!isSuccess)
                    {
                        if (!isHeroPushButton && heroInput.GetIsPushSouthBottun())
                        {
                            heroButtonAnimation.Stop();
                            heroButtonPressAnimation.PlayPushButton();

                            isHeroPushButton = true;
                        }

                        if (!isWomenPushButton && womenInput.GetIsPushSouthBottun())
                        {
                            womenButtonAnimation.Stop();
                            womenButtonPressAnimation.PlayPushButton();

                            isWomenPushButton = true;
                        }

                        if (isHeroPushButton && isWomenPushButton)
                        {
                            Play();
                        }
                    }
                    break;
                }
            case 1://プレイヤーのどちらかがボタンを押したら
                {
                    if (!isHeroPushButton && !isWomenPushButton)
                    {
                        heroButtonAnimation.Play();
                        womenButtonAnimation.Play();

                        if (!isSuccess)
                        {
                            if (heroInput.GetIsPushSouthBottun())
                            {
                                heroButtonAnimation.Stop();
                                womenButtonAnimation.Stop();
                                heroButtonPressAnimation.PlayPushButton();
                                isHeroPushButton = true;

                                Play();
                            }
                            else if (womenInput.GetIsPushSouthBottun())
                            {
                                heroButtonAnimation.Stop();
                                womenButtonAnimation.Stop();
                                womenButtonPressAnimation.PlayPushButton();
                                isWomenPushButton = true;

                                Play();
                            }
                        }
                    }
                    break;
                }
            case 2://プレイヤー1(勇者)が押したら
                {
                    if (!isHeroPushButton)
                    {
                        heroButtonAnimation.Play();

                        if (!isSuccess && heroInput.GetIsPushSouthBottun())
                        {
                            heroButtonAnimation.Stop();
                            heroButtonPressAnimation.PlayPushButton();
                            isHeroPushButton = true;

                            Play();
                        }
                    }
                    break;
                }
            case 3://プレイヤー2(魔王の娘)が押したら
                {

                    if (!isWomenPushButton)
                    {
                        womenButtonAnimation.Play();

                        if (!isSuccess && womenInput.GetIsPushSouthBottun())
                        {
                            womenButtonAnimation.Stop();
                            womenButtonPressAnimation.PlayPushButton();
                            isWomenPushButton = true;

                            Play();
                        }
                    }
                    break;
                }
        }
    }
    public bool GetIsStop() { return isStop; }
}