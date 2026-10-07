using UnityEngine;

public class ObjectFallAoe : MonoBehaviour,IAoe
{
    [SerializeField, Header("目標")]
    private Transform[] target = null;

    [Header("オブジェクト設定")]
    [SerializeField, Header("攻撃オブジェクト")]
    private GameObject attackObj = null;
    [SerializeField]
    private GameObject fireBall = null;
    [SerializeField, Header("追従予兆オブジェクト")]
    private GameObject omenObj = null;
    [SerializeField]
    private Renderer rend = null;
    [SerializeField, Header("予兆アニメーション")]
    private Animator omenAnim = null;
    [SerializeField, Header("エフェクト管理オブジェクト")]
    private EffectManager effectManager = null;
    [SerializeField,Header("カメラシェイク")]
    private CameraShake cameraShake = null;
    [SerializeField]
    private SEManager seManager = null;
    [SerializeField]
    private StageRubbles stageRubbles = null;

    //外から設定した変数を保存する用
    private float omenTime = 0;
    private float attackTime = 0;
    private float delayTime = 0;
    private float size = 0;
    //private int playerNumber = 0;
    private int posNumber = 0;

    //秒数カウント用
    private float countOmen = 0;
    private float countAttack = 0;
    private float countDelay = 0;

    //フラグ管理
    private bool isStart = false;
    private bool isDelay = false;
    private bool isAttack = false;
    private bool isStop = false;

    private Vector3 omenStartSize;
    private Vector3 omenTargetSize;

    private void Start()
    {
        //すべてのオブジェクトを非表示
        attackObj.SetActive(false);
        omenObj.SetActive(false);
        fireBall.SetActive(false);
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

    private void Starting()
    {
        OmenTrack();
        countOmen += Time.unscaledDeltaTime;
        if (countOmen >= omenTime)
        {
            countOmen = 0;
            //アニメーション開始
            omenAnim.SetBool("play", true);
            rend.material.color = Color.red;
            isDelay = true;
            isStart = false;
            seManager.PlayBossMeteorFallSE();
        }
    }

    private void Delay()
    {
        PlayOmen();
        countDelay += Time.unscaledDeltaTime;
        if (countDelay >= delayTime)
        {
            AnimStop();//アニメーションストップ
            omenObj.SetActive(false);
            fireBall.SetActive(false);
            countDelay = 0;
            AttackStart();
            isAttack = true;
            isDelay = false;
        }
    }

    private void PlayOmen()
    {
        if (!fireBall.activeSelf)
        {
            fireBall.SetActive(true);
        }

        if (delayTime <= 0f)
        {
            fireBall.transform.position = omenObj.transform.position;
            return;
        }

        float t = countDelay / delayTime;
        t = Mathf.Clamp01(t);

        fireBall.transform.position = Vector3.Lerp(omenObj.transform.position + new Vector3(0.0f, 10.0f, 0.0f),
                                                   omenObj.transform.position + new Vector3(0,fireBall.transform.localScale.y / 2,0), t);
    }
    private void AttackStart()
    {
        //攻撃時のオブジェクト表示
        if(!attackObj.activeSelf)
        {
            attackObj.SetActive(true);
        }

        attackObj.transform.position = omenObj.transform.position;
        Vector3 pos = omenObj.transform.position + new Vector3(0,1,0);
        effectManager.CreateEffect(2, pos, size);
        seManager.PlayBossExplosionSE();
        StartCoroutine(cameraShake.Shake(0.5f, 0.4f));

        StageRubbles stageRubblesData = Instantiate(stageRubbles, attackObj.transform.position,Quaternion.identity);
        if (stageRubblesData != null)
        {
            stageRubblesData.Play();
        }
    }

    private void Attacking()
    {
        if (!attackObj.activeSelf)
        {
            attackObj.SetActive(true);
        }

        countAttack += Time.unscaledDeltaTime;

        if (countAttack >= 0.2f)
        {
            countAttack = 0;
            attackObj.SetActive(false);
            isAttack = false;
            isStop = true;
        }
    }
    private void OmenTrack()
    {
        if (!omenObj.activeSelf)
        {
            omenObj.SetActive(true);
        }

        omenObj.transform.position = target[posNumber].transform.position;

        if (omenTime <= 0f)
        {
            omenObj.transform.localScale = omenTargetSize;
            return;
        }

        float t = countOmen / omenTime;
        t = Mathf.Clamp01(t);

        //Omen中にサイズを変更
        omenObj.transform.localScale = Vector3.Lerp(omenStartSize, omenTargetSize, t);
    }
    private void AnimStop()
    {
        omenAnim.SetBool("stop", true);
        omenAnim.SetBool("play", false);
        omenAnim.SetBool("stop", false);
    }

    public void AoeStart()
    {
        isStart = true;
    }

    public void SetAoe(float omen, float delay, float attack, float _size,int number)
    {
        //変数を外部から設定
        omenTime = omen;
        delayTime = delay;
        attackTime = attack;
        posNumber = number;

        omenTargetSize = new Vector3(_size, 1, _size);
        omenStartSize = Vector3.zero;

        omenObj.transform.localScale = omenStartSize;
        attackObj.transform.localScale = omenTargetSize;
        fireBall.transform.localScale = Vector3.one * (_size / 5);

        size = _size / 2;
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