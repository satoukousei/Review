using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPull : MonoBehaviour
{
    [Header("自分のPlayerMove")]
    [SerializeField] private PlayerMove playerMove         = null;//自分の入力を受け取るPlayerMove
    [Header("相手のPlayerMove")]
    [SerializeField] private PlayerMove targetMove         = null;//相手の入力を受け取るPlayerMove
    [Header("飛んでいく対象")]
    [SerializeField] private Transform target              = null;//飛んでいく対象
    [Header("相手がキャッチしているかを受け取るPlayerPull")]
    [SerializeField] private PlayerPull targetPull       = null;//相手が引っ張っているかを受け取る
    [Header("引っ張りのデータ")]
    [SerializeField] private PlayerPullData pullData       = null;//数値の情報を取得
    [Header("エフェクトマネージャー")]
    [SerializeField] private EffectManager effectManager   = null;//エフェクトマネージャー
    [Header("アイテムマネージャー")]
    [SerializeField] private GetItemManager item           = null;//アイテムマネージャー
    [Header("暗転処理")]
    [SerializeField] private ScreenFade blackOut             = null;//暗転処理
    [Header("SE")]
    [SerializeField] private SEManager seManager           = null;
    [Header("キャッチ成功時の演出(自分)")]
    [SerializeField] private FadeUI fadeUI                 = null;
    [Header("キャッチ成功時の演出(相手)")]
    [SerializeField] private FadeUI fadeUI2                = null;
    [Header("フェード")]
    [SerializeField] private FadeCreate fade               = null;
    [Header("赤フェード")]
    [SerializeField] private ScreenFade redOut               = null;
    [Header("ボタン演出")]
    [SerializeField] private ObjectSideShake objectSideShake = null;
    [Header("ゲージ")]
    [SerializeField] private GameObject gage               = null;
    [Header("引っ張る相手のリジッドボディ")]
    [SerializeField] private Rigidbody targetRb            = null;
    [Header("ヘルプUI")]
    [SerializeField] private BillBoard billBoard           = null;
    [Header("ハートのQTE")]
    [SerializeField] private HeartQTE heartQTE = null;

    private Rigidbody rb;               //Rigidbodyを保存する変数

    private float distance  = 0;        //ターゲットとの距離を保存する変数
    private float countTime = 0;        //引っ張り可能時間を測る変数
    private float p1Time = 10;          //プレイヤー1のリングの距離
    private float p2Time = 10;          //プレイヤー2のリングの距離
    private float index = 1;            //移動速度の補正値

    private bool isPulling = false;     //引っ張り中フラグ
    private bool canPull = false;       //引っ張り可能フラグ
    private bool canCatch   = false;    //キャッチ可能フラグ
    private bool isCatch    = false;    //キャッチ成功フラグ
    private bool isSuccessCatch = false;    //キャッチ成功フラグ
    private bool isMissCatch = false;   //キャッチ失敗フラグ
    private bool isBegingPulledMissCatch = false;
    private bool isBeingPulled = false; //引っ張られているフラグ
    private bool p1Catch    = false;    //プレイヤー1がキャッチしたかどうかのフラグ
    private bool p2Catch    = false;    //プレイヤー2がキャッチしたかどうかのフラグ
    private bool playFede = false;      //フェードを再生したかどうかのフラグ
    private bool playFede1 = false;     //フェードを再生したかどうかのフラグ
    private bool isStuck = false;       //壁に引っかかっているかどうかのフラグ
    private bool setStun = false;       //スタンを付与するかどうかのフラグ
    private bool isPullable = false;    //引っ張り可能かどうかのフラグ
    private bool isStopAoe = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //Rigidbodyを取得する
    }
    private void Update()
    {
        if (!fade.IsStartFadeEnd)
        {
            return;
        }
        CheckPull();//引っ張りの処理
        CheckCatch();//キャッチの処理

        if (distance >= pullData.GetCanPullDistance())
        {
            if (!isPullable)
            {
                isPullable = true;
            }
        }
        else
        {
            if (isPullable)
            {
                isPullable = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //ターゲットとの距離を測る
        distance = Vector3.Distance(target.position, transform.position);

        if (isPulling)
        {
            Pulling();//引っ張る処理
        }
    }
    private void CheckCatch()
    {
        if (canCatch)//キャッチ可能状態なら
        {
            if (playerMove.GetIsPushSouthBottun() && !p1Catch)//プレイヤー1がボタンを押したら
            {
                p1Time = heartQTE.GetDistance(0);               //ハートの距離を取得
                p1Catch = true;                               //キャッチしたフラグを立てる
            }
            if (targetMove.GetIsPushSouthBottun() && !p2Catch)//プレイヤー2がボタンを押したら
            {
                p2Time = heartQTE.GetDistance(1);               //ハートの距離を取得
                p2Catch = true;                               //キャッチしたフラグを立てる
            }
        }
        else
        {
            p1Time = 10;
            p2Time = 10;
            p1Catch = false;
            p2Catch = false;
        }
    }
    private void CheckPull()
    {
        if (targetMove.GetIsPushSouthBottun() && !canPull)
        {
            if (!targetPull.GetCanPull() && isPullable)//引っ張り可能距離内に入ったら
            {
                canPull = true;
                Vector3 dir = rb.position - targetRb.position;//ターゲットへの方向を求める
                dir.Normalize();                                   //方向ベクトルを正規化する 
                targetRb.rotation = Quaternion.LookRotation(dir);  //ターゲットの方向を向く
                targetMove.SetStunFlag(true);                      //スタンを付与する
                billBoard.SetUIActive(true);
            }
        }

        if (canPull && !isPulling)//引っ張り可能状態なら
        {
            //自分がボタンを押したら
            if (playerMove.GetIsPushSouthBottun())
            {
                isStopAoe = true;

                isPulling = true;                     //自分の引っ張り中フラグを立てる
                targetPull.SetIsBeingPulled(true);    //相手の引っ張られているフラグを立てる
                blackOut.SetFadeImage();              //暗転
                gage.SetActive(false);
                targetRb.linearVelocity = Vector3.zero;
                targetRb.angularVelocity = Vector3.zero;
                targetRb.isKinematic = true;

                billBoard.SetUIActive(false);//デバッグ用imageを非表示にする
            }

            if (!setStun)
            {
                countTime += Time.deltaTime;
                if (countTime >= pullData.GetCanPullTime())//引っ張り可能時間を過ぎたら
                {
                    countTime = 0;
                    canPull = false;                      //引っ張り可能時間を過ぎたら引っ張り不可にする
                    targetMove.SetStunFlag(false);        //スタンを解除する

                    billBoard.SetUIActive(false);         //デバッグ用imageを非表示にする
                }
            }
        }
    }
    #region
    private void Pulling()//引っ張り中の処理
    {
        playerMove.SetStunFlag(true);//自分をスタンさせる

        Vector3 dir = rb.position - targetRb.position;      //ターゲットへの方向を求める
        dir.y = 0f;                                         //Y軸の影響を受けないようにする
        dir.Normalize();                                    //方向ベクトルを正規化する

        rb.MoveRotation(Quaternion.LookRotation(-dir));     //自分の向きをターゲットから向くようにする
        targetRb.MoveRotation(Quaternion.LookRotation(dir));//ターゲットの向きを自分の方向にする

        Vector3 newPos = targetRb.position;                 //ターゲットの現在位置を保存する
        newPos += index * pullData.GetMoveSpeed() * Time.fixedDeltaTime * dir;//ターゲットを引っ張る
        
        targetRb.MovePosition(newPos);                      //ターゲットを引っ張る

        if (distance <= pullData.GetCatchDistance())        //キャッチ可能距離に入ったら
        {
            canCatch = true;
        }

        if (distance < pullData.GetSlowDistance())          //ターゲットに近づいたら時間を遅くする
        {
            Time.timeScale = pullData.GetSlowTime();        //時間を遅くする
            index = pullData.GetCatchDelection();           //移動速度を遅くする
        }
        else
        {
            index = 1;//移動速度の補正値を元に戻す
        }

        if (p1Catch)
        {
            if (p1Time > pullData.GetMaxCatchDistance() || p1Time < pullData.GetMinCatchDistance())
            {
                heartQTE.Miss(0);
            }
            else
            {
                if (!playFede)
                {
                    fadeUI.PlayFade();
                    seManager.PlayPlayerCatchSuccessSE();//キャッチ成功SEを再生する
                    playFede = true;
                }
            }
        }
        else
        {
            heartQTE.MoveHeart(0);//ハートを動かす
        }

        if (p2Catch)
        {
            if (p2Time > pullData.GetMaxCatchDistance() || p2Time < pullData.GetMinCatchDistance())
            {
                heartQTE.Miss(1);
            }
            else
            {
                if (!playFede1)
                {
                    fadeUI2.PlayFade();
                    seManager.PlayPlayerCatchSuccessSE();//キャッチ成功SEを再生する
                    playFede1 = true;
                }
            }
        }
        else
        {
            heartQTE.MoveHeart(1);//ハートを動かす
        }

        if (p1Catch && p2Catch)
        {
            //合計距離がキャッチ範囲内なら
            if (p1Time <= pullData.GetMaxCatchDistance() && p1Time >= pullData.GetMinCatchDistance() &&
                p2Time <= pullData.GetMaxCatchDistance() && p2Time >= pullData.GetMinCatchDistance())
            {
                isSuccessCatch = true;
                heartQTE.Success();
            }
        }

        float stopDistance;
        if (isSuccessCatch)
        {
            stopDistance = pullData.GetStopDistance();
        }
        else
        {
            stopDistance = pullData.GetMissStopDistance();
        }

        if (stopDistance >= 0.0f&& distance <= stopDistance)//ターゲットに近づいたら
        {
            PullStop();//引っ張りを終了する

            //お互いにノックバックさせる
            if (targetPull.GetIsStuck())
            {
                targetRb.position += dir * pullData.GetKnockBackPower();
                rb.position += dir * pullData.GetKnockBackPower();
            }
        }
    }
    #endregion
    private void PullStop()//引っ張り終了処理
    {
        gage.SetActive(true);
        isCatch = isSuccessCatch;//キャッチ成功フラグを立てる

        if (isCatch)//キャッチ成功なら
        {
            effectManager.CreateEffect(0, (transform.position + target.position) / 2, 1);//キャッチ成功エフェクトを生成する
            StartCoroutine(PullStopDelay(pullData.GetCatchDelayTime()));                 //引っ張り停止遅延コルーチンを開始する
            item.BonusPointScore();      //ポイントボーナスを与える
        }
        else//キャッチ失敗なら
        {
            isMissCatch = true;
            targetPull.SetBegingMissCatch(true);
            StartCoroutine(MissCatchDelay(pullData.GetStunTime()));
            Time.timeScale = 1;               //時間を元に戻す
            redOut.SetFadeImage();
            redOut.StartFadeOut();
            objectSideShake.Shake();
            item.DebuffPointScore();          //ポイントデバフを与える
        }
        countTime = 0;                         //引っ張り可能時間をリセットする
        isPulling = false;                     //引っ張りを終了する
        canPull = false;                       //引っ張り可能を解除する
        canCatch = false;                      //キャッチ可能を解除する
        targetPull.SetIsBeingPulled(false);    //引っ張られているフラグを解除する

        heartQTE.ResetQte();                  //ハートをリセットする

        blackOut.StartFadeOut();               //暗転解除
        playFede = false;
        playFede1 = false;
        isStuck = false;
        targetRb.isKinematic = false;
    }
    private IEnumerator PullStopDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        isCatch = false;                  //キャッチ成功フラグをリセットする
        isSuccessCatch = false;           //キャッチ成功フラグをリセットする
        Time.timeScale = 1;               //時間を元に戻す
        playerMove.SetStunFlag(false);    //スタンを解除する
        targetMove.SetStunFlag(false);    //スタンを解除する
        isStopAoe = false;
    }
    private IEnumerator MissCatchDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        isMissCatch = false;              //キャッチ失敗フラグをリセットする
        targetPull.SetBegingMissCatch(false);
        playerMove.SetStunFlag(false);    //スタンを解除する
        targetMove.SetStunFlag(false);    //スタンを解除する
        isStopAoe = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isStuck = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isStuck = false;
        }
    }

    private void SetIsBeingPulled(bool flag) { isBeingPulled = flag; }
    public void SetBegingMissCatch(bool flag){ isBegingPulledMissCatch = flag; }
    public void SetCanStun (bool flag) { setStun = flag; }
    public bool GetIsStuck() { return isStuck; }//壁に引っかかっているかどうかを返す関数
    public bool GetCanPull() { return canPull; }//引っ張り可能かどうかを返す関数
    public bool GetIsPulling() { return isPulling; }//引っ張り中かどうかを返す関数
    public bool GetIsBeingPulled() { return isBeingPulled; }//引っ張られているかどうかを返す関数
    public bool GetIsCatch() { return isCatch; }//キャッチしているかどうかを返す関数
    public bool GetIsMissCatch() { return isMissCatch; }//キャッチ失敗しているかどうかを返す関数
    public bool GetIsBegingPulledMissCatch() { return isBegingPulledMissCatch; }
    public bool GetIsPullable() { return isPullable; }//引っ張り可能かどうかを返す関数
    public bool GetIsStopAoe() { return isStopAoe; }
}