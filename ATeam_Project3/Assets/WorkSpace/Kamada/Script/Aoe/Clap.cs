using UnityEngine;
using System.Collections;

public class Clap : MonoBehaviour,IAoe
{
    [SerializeField, Header("プレイヤー")]
    private Transform[] player = null;
    [Header("オブジェクト設定")]
    [SerializeField, Header("攻撃オブジェクト")]
    private GameObject attackObj = null;
    [SerializeField]
    private GameObject hand = null;
    [SerializeField]
    private GameObject leftHand = null;
    [SerializeField]
    private GameObject rightHand = null;
    [SerializeField]
    private Transform[] handPos = null;
    [SerializeField, Header("追従予兆オブジェクト")]
    private GameObject omenObj = null;
    [SerializeField]
    private Renderer rend = null;
    [SerializeField, Header("予兆アニメーション1")]
    private Animator omenAnim = null;
    [SerializeField]
    private EffectManager effectManager = null;

    //外から設定した変数を保存する用
    private float omenTime = 0;
    private float attackTime = 0;
    private float delayTime = 0;
    private int playerNumber = 0;
    private float rotateSpeed = 0;
    private float stopAngle = 0;

    //秒数カウント用
    private float countOmen = 0;
    private float countAttack = 0;
    private float countDelay = 0;

    //フラグ管理
    private bool isStart = false;
    private bool isDelay = false;
    private bool isAttack = false;
    private bool isStop = false;

    private void Awake()
    {
        //すべてのオブジェクトを非表示
        attackObj.SetActive(false);
        omenObj.SetActive(false);
        leftHand.SetActive(false);
        rightHand.SetActive(false);
    }
    private void Update()
    {
        if (isStart)//予兆時間カウント
        {
            Starting();
        }

        if (isDelay)//フェード時間カウント
        {
            Delay();
        }

        if (isAttack)//攻撃時間カウント
        {
            Attacking();
        }
    }

    void Starting()
    {
        OmenTrack();
        countOmen += Time.unscaledDeltaTime;
        float angle = Mathf.Abs(stopAngle - omenObj.transform.eulerAngles.y);
        if (countOmen >= omenTime && angle <= 0.2)
        {
            countOmen = 0;
            rend.material.color = Color.red;
            isDelay = true;
            isStart = false;
        }
    }

    void Delay()
    {
        //PlayOmen();
        //アニメーション開始
        if (!omenObj.activeSelf)
        {
            omenObj.SetActive (true);
        }

        omenAnim.SetBool("play", true);
        countDelay += Time.unscaledDeltaTime;
        if (countDelay >= delayTime)
        {
            AnimStop();//アニメーションストップ
            omenObj.SetActive(false);
            countDelay = 0;
            isAttack = true;
            isDelay = false;
        }
    }
    void Attacking()
    {
        AttackStart();
        countAttack += Time.unscaledDeltaTime;
        if (countAttack >= attackTime)
        {
            attackObj.transform.position = omenObj.transform.position;
            StartCoroutine(AttackStart(0.5f));
            effectManager.CreateEffect(1, omenObj.transform.position,1);

            countAttack = 0;
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            isAttack = false;
            isStop = true;
        }
    }
    private IEnumerator AttackStart(float _time)
    {
        attackObj.SetActive(true);
        yield return new WaitForSeconds(_time);
        attackObj.SetActive(false);
    }

    void PlayOmen()
    {
        if (!leftHand.activeSelf && !rightHand.activeSelf)
        {
           leftHand.SetActive(true);
            rightHand.SetActive(true);
        }
    }
    void AttackStart()
    {
        if (attackTime <= 0f)
        {
            leftHand.transform.position = hand.transform.position;
            rightHand.transform.position = hand.transform.position;
            return;
        }

        if (!leftHand.activeSelf)
        {
            leftHand.SetActive(true);
            rightHand.SetActive(true);
        }

        float t = countAttack / attackTime;
        t = Mathf.Clamp01(t);

        leftHand.transform.position = Vector3.Lerp(handPos[0].position, hand.transform.position, t);
        rightHand.transform.position = Vector3.Lerp(handPos[1].position, hand.transform.position, t);
    }
    void OmenTrack()
    {
        //予兆オブジェクトが隠れているとき表示
        if (!omenObj.activeSelf)
        {
            omenObj.SetActive(true);
        }
        //プレイヤー追従
        Vector3 size = player[playerNumber].transform.localScale;
        Vector3 playerPos = player[playerNumber].transform.position;
        playerPos.y -= size.y / 2;
        omenObj.transform.position = playerPos;

        Vector3 angle = omenObj.transform.eulerAngles;
        angle.y += Mathf.Repeat(rotateSpeed * Time.unscaledDeltaTime, 360f);
        omenObj.transform.eulerAngles = angle;

        hand.transform.eulerAngles = omenObj.transform.eulerAngles;
        hand.transform.position = omenObj.transform.position + new Vector3(0,1,0);

        Vector3 leftAngle = leftHand.transform.eulerAngles;
        Vector3 rightAngle = rightHand.transform.eulerAngles;
        leftAngle.y = omenObj.transform.eulerAngles.y;
        rightAngle.y = omenObj.transform.eulerAngles.y;
        leftHand.transform.eulerAngles = leftAngle;
        rightHand.transform.eulerAngles = rightAngle;

        leftHand.transform.position = handPos[0].position;
        rightHand.transform.position = handPos[1].position;
    }
    void AnimStop()
    {
        omenAnim.SetBool("stop", true);
        omenAnim.SetBool("play", false);
        omenAnim.SetBool("stop", false);
    }

    public void AoeStart()
    {
        isStart = true;
        PlayOmen();
    }

    public void SetAoe(float omen, float delay, float attack,int number,float speed,float angle)
    {
        //変数を外部から設定
        omenTime = omen;
        delayTime = delay;
        attackTime = attack;
        playerNumber = number;
        rotateSpeed = speed;
        stopAngle = angle;
        hand.transform.eulerAngles = Vector3.zero;
    }

    public bool IsFinished()
    {
        return isStop;
    }

    public void OnFinish()
    {
        Destroy(gameObject);
    }
}