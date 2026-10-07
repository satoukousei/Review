using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiBarrageAoe : MonoBehaviour, IAoe
{
    private static WaitForSeconds _waitForSeconds1_0 = new WaitForSeconds(1.0f);
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private Transform[] wayPoint;
    [SerializeField] private ParticleSystem[] magicSquar;
    [SerializeField] private SEManager seManager;

    private string wayPointIndex = "";

    private int bulletCount = 0;

    private float moveSpeed = 0;
    private float size = 0;

    private bool isPlay = false;
    private bool isStop = false;

    private MoveBullet[] moveBullets;

    private void Start()
    {
        if (bulletObject != null)
        {
            bulletObject.SetActive(false);
        }

        for (int i = 0; i < magicSquar.Length; i++)
        {
            magicSquar[i].Stop();//最初はエフェクト停止
        }
    }

    public void SetMultiBarrageAoe(int spawnPos, string _spawnIndex, float _speed, float _size)
    {
        wayPointIndex = _spawnIndex;
        moveSpeed = _speed;
        size = _size;
        SetPosition(spawnPos);
    }

    public void PlayMultiBarrageAoe()
    {
        if (isPlay)
        {
            return;
        }
        isPlay = true;
        isStop = false;
        

        for (int i = 0; i < magicSquar.Length; i++)
        {
            magicSquar[i].Play();
        }

        StartCoroutine(ShootMultiBarrage());
    }

    private void SetPosition(int posNumber)
    {
        if(posNumber < 0 || posNumber >= spawnPoint.Length)
        {
            return;
        }
        startPoint.position = spawnPoint[posNumber].position + new Vector3(0,(size - 1.0f) / 2.0f,0);
        startPoint.localEulerAngles = spawnPoint[posNumber].localEulerAngles;
    }

    private IEnumerator ShootMultiBarrage()
    {
        yield return _waitForSeconds1_0;//エフェクト再生待機
        moveBullets = new MoveBullet[wayPointIndex.Length];
        bulletCount = 0;

        for (int i = 0; i < wayPointIndex.Length; i++)
        {
            int index = wayPointIndex[i] - '0';
            SetBullet(index);
        }

        for (int i = 0; i < bulletCount; i++)
        {
            moveBullets[i].PlayMove();
            seManager.PlayBossFireBallSE();
        }

        EffectStop();
        
    }

    private void SetBullet(int posNumber)
    {
        if (posNumber < 0 || posNumber >= wayPoint.Length)
        {
            return;
        }

        Vector3 dir = wayPoint[posNumber].position - startPoint.position;
        dir.Normalize();
        Vector3 pos = startPoint.position + dir * 2f;
        Quaternion rot = Quaternion.LookRotation(dir);

        GameObject bullet = Instantiate(bulletObject, pos, rot);
        bullet.transform.localScale = new Vector3(size, size, size);
        bullet.SetActive(true);

        MoveBullet mb = bullet.GetComponent<MoveBullet>();

        moveBullets[bulletCount] = mb;
        mb.SetMove(moveSpeed, dir);

        bulletCount++;
    }
    private void EffectStop()//エフェクト停止
    {
        if (isStop)
        {
            return;
        }

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
    public bool IsFinished()
    {
        return isStop;
    }

    public void OnFinish()
    {
        Destroy(gameObject);
    }
}