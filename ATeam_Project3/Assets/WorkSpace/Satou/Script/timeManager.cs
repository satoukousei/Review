using UnityEngine;
using TMPro;

public class timeManager : MonoBehaviour
{
    [SerializeField]
    private FadeCreate fade;
    [SerializeField]
    private float maxTime = 0.0f;//指定した秒数
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private GameObject timeObject;
    [SerializeField]
    private SaveScore saveScore;
    [SerializeField]
    private float voiceTime = 0.0f;//ボイス再生時間
    [SerializeField]
    private SEManager VoiceSE;

    private bool  isEnd = false;
    private float timer = 0.0f; //タイマー
    private float voiceTimer = 0.0f;//ボイス用タイマー
    private bool isVoiceStart = false;
    private bool isVoiceEnd = false;

    private void Start()
    {
        timer = maxTime;
        timeText.text = "Time:" + ((int)timer).ToString();
        timeObject.SetActive(false);
    }
    void Update()
    {
        if (fade.IsStartFadeEnd)
        {
            CountTime();

            if (!timeObject.activeSelf)
            {
                timeObject.SetActive(true);
            }
        }

        if (!isVoiceStart)
        {
            isVoiceStart = true;
            VoiceSE.PlaySelectSE();
        }
    }

    private void CountTime()//指定した時間を計測し、指定した秒数に達したらフェードしてシーン遷移
    {
        if (timer <= 0 /*&& !isEnd*/)
        {
            voiceTimer += Time.deltaTime;

            if (voiceTimer >= voiceTime)
            {

                if (!isVoiceEnd)
                {
                    isVoiceEnd = true;
                    VoiceSE.PlayBossVoiceSE();
                }

                isEnd = true;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            timeText.text = "Time:" + ((int)timer).ToString();
        }
    }

    public bool GetIsEnd()
    {
        return isEnd; 
    }
}
