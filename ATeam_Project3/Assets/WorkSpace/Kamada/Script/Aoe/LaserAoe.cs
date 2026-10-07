using UnityEngine;

public class LaserAoe : MonoBehaviour,IAoe
{
    [Header("レーザー攻撃の設定")]
    [SerializeField] private Transform[] spawnPoint = null;                //スポーン位置
    [Header("レーザー攻撃のオブジェクトとエフェクト")]
    [SerializeField] private GameObject laserObject = null;                //レーザーオブジェクト
    [Header("レーザー攻撃のエフェクト")]
    [SerializeField] private GameObject laserEffect = null;                //レーザーエフェクトオブジェクト
    [Header("レーザー攻撃のパーティクル")]
    [SerializeField] private ParticleSystem[] laserParicle = null;         //レーザーパーティクル
    [Header("キャノンのオブジェクトとエフェクト")]
    [SerializeField] private GameObject cannonObject = null;               //キャノンオブジェクト
    [Header("キャノンスポーンエフェクト")]
    [SerializeField] private GameObject cannonSpawnEffect = null;          //キャノンスポーンエフェクトオブジェクト
    [Header("キャノンスポーンエフェクトのパーティクル")]
    [SerializeField] private ParticleSystem cannonSpawnParticle = null;    //キャノンスポーンエフェクトのパーティクル
    [Header("チャージエフェクト")]
    [SerializeField] private GameObject chargeEffect = null;               //チャージエフェクトオブジェクト
    [Header("チャージエフェクトのパーティクル")]
    [SerializeField] private ParticleSystem[] chargeParticle = null;       //チャージエフェクトのパーティクル
    [Header("チャージ完了エフェクト")]
    [SerializeField] private GameObject chargeCompletedEffect = null;      //チャージ完了エフェクトオブジェクト
    [Header("チャージ完了エフェクトのパーティクル")]
    [SerializeField] private ParticleSystem chargeCompletedParticle = null;//チャージ完了エフェクトのパーティクル
    [Header("アニメーター")]
    [SerializeField] private Animator anim = null;                         //アニメーター
    [SerializeField] private SEManager seManager;
    [SerializeField] private Collider lassrCollider = null;


    private bool isPlay = false;         //再生開始フラグ
    private bool isCharge = false;       //チャージ中フラグ
    private bool isAttack = false;       //攻撃中フラグ
    private bool isPlayStopCheck = false;//攻撃終了後の処理開始フラグ
    private bool isStop = false;         //攻撃終了フラグ
    private bool isEffectStop = false;   //攻撃終了後のエフェクト停止フラグ

    private bool testPlay = false;
    private bool testStop = false;

    private float countChargeTime = 0f;//チャージ時間カウント用
    private float chargeTime = 0f;     //チャージ時間保存用
    private float countAttackTime = 0f;//攻撃時間カウント用
    private float attackTime = 0f;     //攻撃時間保存用
    private float delayTime = 0f;      //攻撃終了後の遅延時間カウント用


    private void Start()
    {
        //最初は全てのオブジェクトを非表示にしておく
        laserObject.SetActive(false);
        laserEffect.SetActive(false);
        cannonObject.SetActive(false);
        chargeEffect.SetActive(false);
        cannonSpawnEffect.SetActive(false);
        chargeCompletedEffect.SetActive(false);


        //全てのパーティクルを停止しておく
        for (int i = 0; i < laserParicle.Length; i++)
        {
            laserParicle[i].Stop();
        }

        for (int i = 0; i < chargeParticle.Length; i++)
        {
            chargeParticle[i].Stop();
        }
    }

    private void Update()
    {
        //再生開始前は処理しない
        if (!isPlay)
        {
            return;
        }

        //チャージ中の処理
        if (isCharge)
        {
            Charge();
        }

        //攻撃中の処理
        if (isAttack)
        {
            Attack();
        }

        //攻撃終了後の処理
        if (isPlayStopCheck)
        {
            StopCheck();
        }
    }
    /// <summary>
    /// レーザー攻撃のチャージ処理を行う
    /// </summary>
    private void Charge()
    {
        //キャノンがスポーンしていない場合はスポーンさせる
        if (!cannonObject.activeSelf)
        {            
            cannonSpawnEffect.SetActive(true);
            cannonSpawnParticle.Play();
            cannonObject.SetActive(true);
            chargeEffect.SetActive(true);
            anim.SetBool("Play",true);
            if (!testPlay)
            {
                seManager.PlayBossBeamSelectingSE();
                testPlay = true;
            }
        }


        //チャージエフェクトが再生されていない場合は再生させる
        if (!chargeParticle[0].isPlaying)
        {
            for (int i = 0; i < chargeParticle.Length; i++)
            
            {           
                chargeParticle[i].Play();
            }
        }

        //キャノンスポーンエフェクトが再生されていない場合はエフェクトを非表示にする
        if (!cannonSpawnParticle.isPlaying)
        {
            cannonSpawnEffect.SetActive(false);
        }

        //チャージ時間をカウントする
        countChargeTime += Time.unscaledDeltaTime;

        //チャージ完了のエフェクトを再生する
        if (countChargeTime > chargeTime - 0.5f)
        {
            chargeCompletedEffect.SetActive(true);
            chargeCompletedParticle.Play();
            anim.SetBool("PlayShoot", true);

            if (!testStop)
            {
                seManager.StopBossChargeSE();       
                seManager.PlayBossBeamChargeCompletedSE();
                testStop = true;
            }        

        }


        //チャージ時間が経過したらチャージエフェクトを非表示にして攻撃状態に移行する
        if (countChargeTime > chargeTime)
        {
            chargeEffect.SetActive(false);
            if (chargeParticle[0].isPlaying)
            {
                for (int i = 0; i < chargeParticle.Length; i++)
                {
                    chargeParticle[i].Stop();
                }
            }
            isAttack = true;
            isCharge = false;
        }
    }
    /// <summary>
    /// レーザー攻撃処理を行う
    /// </summary>
    private void Attack()
    {
        //レーザーがスポーンしていない場合はスポーンさせる
        if (!laserObject.activeSelf)
        {
            laserObject.SetActive(true);
            laserEffect.SetActive(true);
        }

        //レーザーエフェクトが再生されていない場合は再生させる
        if (!laserParicle[0].isPlaying)
        {
            for (int i = 0; i < laserParicle.Length; i++)
            {
                laserParicle[i].Play();
            }
        }

        //攻撃時間をカウントする
        countAttackTime += Time.unscaledDeltaTime;

        //攻撃時間が経過したらレーザーエフェクトを非表示にして攻撃終了状態に移行する
        if (countAttackTime > attackTime) {
            lassrCollider.enabled = false;
            if (laserParicle[0].isPlaying)
            {
                for (int i = 0; i < laserParicle.Length; i++)
                {
                    laserParicle[i].Stop();
                }
            }
            isPlayStopCheck = true;
            isAttack = false;

        }
    }
    /// <summary>
    /// レーザー攻撃終了後の処理を行う
    /// </summary>
    private void StopCheck()
    {
        //レーザーエフェクトが再生されていない場合はレーザーエフェクトを非表示にしてキャノンスポーンエフェクトを再生させる
        if (!laserParicle[laserParicle.Length - 1].isPlaying && !isEffectStop)
        {
            laserEffect.SetActive(false);
            cannonSpawnEffect.SetActive(true);
            cannonSpawnParticle.Play();
            isEffectStop = true;
        }

        //キャノンスポーンエフェクトが再生されていて一定時間過ぎたら
        if (isEffectStop)
        {
            delayTime += Time.unscaledDeltaTime;
            if (delayTime >= 0.6f)
            {
                isStop = true;
            }
        }
    }
    /// <summary>
    /// レーザー攻撃のスポーン位置、角度、チャージ時間、攻撃時間を設定する
    /// </summary>
    /// <param name="posNumber">生成座標の数字</param>
    /// <param name="angle">生成角度</param>
    /// <param name="_chargeTime">チャージ時間</param>
    /// <param name="_attackTime">レーザー表示時間</param>
    public void SetLaserAoe(int posNumber, float angle,float size,float _chargeTime,float _attackTime)
    {
        //スポーン位置の設定
        laserObject.transform.position = spawnPoint[posNumber].position;
        cannonObject.transform.position = spawnPoint[posNumber].position;

        laserObject.transform.localScale = new Vector3(laserObject.transform.localScale.x, laserObject.transform.localScale.y, size);

        //レーザーの角度設定
        laserObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        cannonObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //時間の設定
        chargeTime = _chargeTime;
        attackTime = _attackTime;
    }
    public void LaserPlay()
    {
        isPlay = true;
        isCharge = true;
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