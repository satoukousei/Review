using System.Collections;
using UnityEngine;

public class BarrageAoe : MonoBehaviour, IAoe
{
    private static WaitForSeconds _waitForSeconds1_0 = new WaitForSeconds(1.0f);
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private GameObject cannonObject;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private ParticleSystem[] magicSquar;
    [SerializeField] private SEManager seManager;

    private int positionIndex;  //砲台位置
    private int direction;      //回転方向

    private float rotateSpeed;  //砲台回転速度
    private float moveSpeed;    //弾移動速度
    private float shootInterval;//射撃間隔
    private float minAngle;     //砲台最小角度
    private float maxAngle;     //砲台最大角度
    private float size;         //砲台サイズ

    private float currentAngle; //現在の砲台角度
    private float shootTimer;   //射撃間隔タイマー

    private bool isRotateRight; //回転方向
    private bool isPlay;        //砲台回転中
    private bool isStop;        //砲台攻撃終了

    private void Start()
    {
        bulletObject.SetActive(false);
        cannonObject.SetActive(false);

        for (int i = 0; i < magicSquar.Length; i++)
        {
            magicSquar[i].Stop();//最初はエフェクト停止
        }
    }

    private void FixedUpdate()
    {
        if (isPlay)
        {
            RotateCannon();
            CountShotInterval();
        }
    }
    public void SetBarrageAoe(int posIndex, int isRight, float _rotateSpeed,
        float _moveSpeed, float interval, float _minAngle, float _maxAngle,float _size)//砲台設定
    {
        positionIndex = posIndex;

        if(isRight == 0)
        {
            isRotateRight = false;
        }
        else {
            isRotateRight = true;
        }

        rotateSpeed = _rotateSpeed;
        moveSpeed = _moveSpeed;
        shootInterval = interval;
        minAngle = _minAngle;
        maxAngle = _maxAngle;
        size = _size;
    }

    public IEnumerator PlayBarrageAoe()//砲台発射開始
    {
        shootTimer = shootInterval;
        cannonObject.transform.position = spawnPoint[positionIndex].position + new Vector3(0,(size - 1.0f) / 2,0f);
        direction = isRotateRight ? 1 : -1;//回転方向設定

        float offset = (positionIndex == 0) ? 0f : 180f;//砲台位置による角度補正
        currentAngle = isRotateRight ? minAngle + offset : maxAngle + offset;

        cannonObject.transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        cannonObject.SetActive(true);

        for (int i = 0; i < magicSquar.Length; i++)
        {
            magicSquar[i].Play();
        }

        yield return _waitForSeconds1_0;//エフェクト再生待機
        isPlay = true;
        isStop = false;
    }
    private void RotateCannon()//砲台回転
    {
        float offset = (positionIndex == 0) ? 0f : 180f;//砲台位置による角度補正
        float min = minAngle + offset;//最小角度
        float max = maxAngle + offset;//最大角度

        currentAngle += direction * rotateSpeed * Time.fixedUnscaledDeltaTime;
        currentAngle = Mathf.Clamp(currentAngle, min, max);//角度制限

        cannonObject.transform.rotation = Quaternion.Euler(0, currentAngle, 0);

        CheckStop(min, max);
    }

    private void CheckStop(float min, float max)//回転停止チェック
    {
        //回転が最大値・最小値に達したらエフェクト停止
        if (isRotateRight && currentAngle >= max - 0.01f)
        {
            EffectStop();
        }
        else if (!isRotateRight && currentAngle <= min + 0.01f)
        {
            EffectStop();
        }
    }

    private void EffectStop()//エフェクト停止
    {
        if (isStop) return;

        isPlay = false;//回転停止

        for (int i = 0; i < magicSquar.Length; i++)//ループ停止
        {
            ParticleSystem.MainModule main = magicSquar[i].main;
            if (main.loop)
            {
                main.loop = false;
            }
        }

        if (!magicSquar[magicSquar.Length - 1].isPlaying)//最後のエフェクトが再生されていなければ停止
        {
            isStop = true;//砲台攻撃終了
        }
    }
    private void CountShotInterval()//射撃間隔カウント
    {
        shootTimer += Time.fixedUnscaledDeltaTime;

        if (shootTimer >= shootInterval)
        {
            ShootBullet();
            shootTimer = 0f;
        }
    }

    private void ShootBullet()//弾生成
    {
        seManager.PlayBossFireBallSE();
        Vector3 pos = cannonObject.transform.position + cannonObject.transform.forward * 2f;
        GameObject bullet = Instantiate(bulletObject, pos, cannonObject.transform.rotation);
        bullet.transform.localScale = new Vector3(size, size, size);

        MoveBullet moveBullet = bullet.GetComponent<MoveBullet>();
        moveBullet.SetMove(moveSpeed, cannonObject.transform.forward); bullet.SetActive(true);
        moveBullet.PlayMove();
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