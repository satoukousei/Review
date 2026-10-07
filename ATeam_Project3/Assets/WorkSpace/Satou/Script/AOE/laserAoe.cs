using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class laserAoe : MonoBehaviour,IAoe
{
    [SerializeField]
    private GameObject laserObject;
    [SerializeField]
    private GameObject omen;
    [SerializeField]
    private GameObject omenObject;
    [SerializeField]
    private Renderer rend;
    [SerializeField]
    private GameObject cannon;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform[] movePoint;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private GameObject fireObject;
    [SerializeField]
    private ParticleSystem[] laserEffect;

    [SerializeField]
    private float omenMinSize = 0.5f;//サイズ増減の最小値
    [SerializeField]
    private float omenMaxSize = 1.5f;//最大値
    [SerializeField]
    private float speed = 1.0f;//サイズ増減の速度
    [SerializeField]
    private SEManager seManager;
    [SerializeField]
    private CameraZoom cameraZoom;

    private float rotateSpeed = 0;
    private float omenTime = 0;
    private float laserTime = 0;
    private float delayTime = 0;
    private float moveSpeed = 0;
    private float omenEndSize = 0;

    private float countOmen = 0;
    private float countLaser = 0;
    private float countDelay = 0;

    private bool isOmen = false;
    private bool isAttack = false;
    private bool isDelay = false;
    private bool isStop = false;
    private bool canMove = false;

    private int moveState = 0;

    void Awake()
    {
        omenObject.SetActive(false);
        cannon.SetActive(false);
        laserObject.SetActive(false);
        fire.SetActive(false);
        spawnPoint.transform.position = movePoint[0].transform.position;
    }

    void Update()
    {
        //if (cameraZoom.GetIsPlay())
        //{
        //    omenObject.SetActive(false);
        //    cannon.SetActive(false);
        //    laserObject.SetActive(false);
        //    fire.SetActive(false);
        //
        //for (int i = 0; i < laserEffect.Length; i++)
        //{
        //    laserEffect[i].Stop();
        //}
        //    return;
        //}

        if (isOmen)
        {
            countOmen += Time.deltaTime;
            //カウントが終わって終了時のサイズに近づいたら
            float difference = Mathf.Abs(omen.transform.localScale.z - omenEndSize);
            if (countOmen >= omenTime && difference <= 0.1)
            {
                countOmen = 0;
                isDelay = true;
                animator.SetBool("play", true);
                rend.material.color = Color.yellow;
                isOmen = false;
                //seManager.PlayBossBeamSelectingSE();
            }

            if (canMove)
            {
                LaserMove();
            }
            else
            {
                spawnPoint.transform.position = movePoint[4].transform.position;
            }
        }

        if (isDelay)
        {
            countDelay += Time.deltaTime;

            if(countDelay >= 1.0f)
            {
                omenObject.SetActive(false);
            }

            if (countDelay >= delayTime)
            {
                countDelay = 0;
                animator.SetBool("play", false);
                animator.SetBool("stop", true);
                LaserSpawn();
                isAttack = true;
                isDelay = false;
                //seManager.PlayBossBeamAttackSE();
            }
        }

        if (isAttack)
        {
            countLaser += Time.deltaTime;

            fire.transform.position = laserObject.transform.position;
            fire.transform.eulerAngles = new Vector3(fire.transform.eulerAngles.x, laserObject.transform.eulerAngles.y, fire.transform.eulerAngles.z);
            fire.transform.localScale = laserObject.transform.localScale;
            fireObject.transform.localScale = laserObject.transform.localScale / 10;
            fire.SetActive(true);

            if (!laserObject.activeSelf)
            {
                laserObject.SetActive(true);
                cannon.SetActive(true);
                for(int i = 0; i < laserEffect.Length; i++)
                {
                    if (!laserEffect[i].isPlaying)
                    {
                        laserEffect[i].Play();
                    }
                }
            }

            if (countLaser >= laserTime)
            {
                laserObject.SetActive(false);
                cannon.SetActive(false);
                animator.SetBool("stop", false);
                countLaser = 0;
                isAttack = false;
                StartCoroutine(PullStopDelay(10.0f));
                //for (int i = 0; i < laserEffect.Length; i++)
                //{
                //    ParticleSystem.MainModule main = laserEffect[i].main;
                //    if (main.loop)
                //    {
                //        main.loop = false;
                //    }
                //}

                //if (!laserEffect[5].isPlaying)
                //{
                //    laserObject.SetActive(false);
                //    cannon.SetActive(false);
                //    animator.SetBool("stop", false);
                //    countLaser = 0;
                //    isAttack = false;
                //    StartCoroutine(PullStopDelay(10.0f));
                //}
            }
        }
    }

    private void FixedUpdate()
    {
        if (isOmen)
        {
            OmenRotate();
            OmenScale();//サイズ変更処理呼び出し
        }
    }

    private IEnumerator PullStopDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fire.SetActive(false);
        isStop = true;
    }

    public void StartAoe() 
    {
        OmenSpawn();
        isOmen = true;
    }

    void OmenSpawn()
    {
        omen.transform.position = spawnPoint.position + new Vector3(0, spawnPoint.localScale.y, 0);
        omen.transform.localScale *= omenMinSize;
        omenObject.SetActive(true);
        cannon.SetActive(true);
    }
    void OmenRotate()
    {
        if (!omen.activeSelf)
        {
            omen.SetActive(true);
            cannon.SetActive(true);
        }

        omen.transform.position = spawnPoint.position + new Vector3(0, spawnPoint.localScale.y, 0);
        Vector3 angle = omen.transform.eulerAngles;
        angle.y += Mathf.Repeat(rotateSpeed * Time.deltaTime, 360f);
        omen.transform.eulerAngles = angle;
        //omen.transform.localScale = Vector3.Lerp(omen.transform.localScale, omenEndSize, t);
    }
    void OmenScale()//サイズ変更処理
    {
        float t = Mathf.PingPong(countOmen * speed, 1f);
        float scale = Mathf.Lerp(omenMinSize, omenMaxSize, t);
        omen.transform.localScale = new Vector3(omenMaxSize, 1f, scale);
    }
    private void LaserSpawn()
    {
        if (!laserObject.activeSelf)
        {
            laserObject.SetActive(true);
            cannon.SetActive(true);
        }

        Vector3 angle = omen.transform.eulerAngles;
        laserObject.transform.position = omen.transform.position;
        laserObject.transform.eulerAngles = new Vector3(angle.x, angle.y, laserObject.transform.eulerAngles.z);

        Vector3 scale = omen.transform.localScale;
        scale.y = scale.z;
        laserObject.transform.localScale = scale;
    }

  public void SetAoe(float omenT, float laserT, float delayT, float speed,float angle,float move,float _moveSpeed,float size)
    {

        omen.transform.eulerAngles = new Vector3(omen.transform.eulerAngles.x, angle, omen.transform.eulerAngles.z);
        omenTime = omenT;
        laserTime = laserT;
        delayTime = delayT;
        rotateSpeed = speed;
        moveSpeed = _moveSpeed;
        omenEndSize = size;//終了時のサイズを指定Csvでとってくる
        if (move == 1)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    void LaserMove()
    {
        if (moveState == 0)
        {
            Vector3 dir = movePoint[0].position - spawnPoint.position;
            dir.Normalize();
            spawnPoint.position += moveSpeed * Time.deltaTime * dir;

            if (Mathf.Abs(Vector3.Distance(spawnPoint.position, movePoint[0].position)) <= 0.5)
            {
                moveState++;
            }
        }
        else if (moveState == 1)
        {
            Vector3 dir = movePoint[1].position - spawnPoint.position;
            dir.Normalize();
            spawnPoint.position += moveSpeed * Time.deltaTime * dir;
            if (Mathf.Abs(Vector3.Distance(spawnPoint.position, movePoint[1].position)) <= 0.5)
            {
                moveState++;
            }
        }
        else if (moveState == 2)
        {
            Vector3 dir = movePoint[2].position - spawnPoint.position;
            dir.Normalize();
            spawnPoint.position += moveSpeed * Time.deltaTime * dir;
            if (Mathf.Abs(Vector3.Distance(spawnPoint.position, movePoint[2].position)) <= 0.5)
            {
                moveState++;
            }
        }
        else if (moveState == 3)
        {
            Vector3 dir = movePoint[3].position - spawnPoint.position;
            dir.Normalize();
            spawnPoint.position += moveSpeed * Time.deltaTime * dir;
            if (Mathf.Abs(Vector3.Distance(spawnPoint.position, movePoint[3].position)) <= 0.5)
            {
                moveState = 0;
            }
        }
    }

    public float GetMoveSpeed() { return moveSpeed; }
    public bool GetIsOmen() { return isOmen; }
    public bool GetIsStop() { return isStop; }
    public bool GetCanMove() { return canMove; }

    public bool IsFinished()
    {
        return isStop;
    }

    public void OnFinish()
    {
        Destroy(gameObject);
    }
}
