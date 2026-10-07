
using UnityEngine;
using UnityEngine.InputSystem;

public class WaveAoe : MonoBehaviour
{
    [SerializeField]
    private GameObject waveObject;
    [SerializeField]
    private GameObject omen;
    [SerializeField]
    private GameObject omenObject;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Animator animator;

    private float moveSpeed = 0;
    private float omenTime = 0;
    private float waveTime = 0;
    private float delayTime = 0;

    private float countOmen = 0;
    private float countDelay = 0;

    private bool isOmen = false;
    private bool isAttack = false;
    private bool isDelay = false;
    private bool isStop = false;

    void Start()
    {
        omenObject.SetActive(false);
        waveObject.SetActive(false);    
    }

    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartAoe();
        }

        if (isOmen)
        {
            countOmen += Time.deltaTime;
            if (countOmen >= omenTime)
            {
                countOmen = 0;
                isDelay = true;
                animator.SetBool("play", true);
                isOmen = false;
            }
        }

        if (isDelay)
        {
            countDelay += Time.deltaTime;
            if (countDelay >= delayTime)
            {
                countDelay = 0;
                animator.SetBool("play", false);
                animator.SetBool("stop", true);
                WaveSpawn();
                isAttack = true;
                isDelay = false;
            }
        }

        if (isAttack)
        {
            WaveMove();
            countOmen += Time.deltaTime;    //予兆時間カウント
            animator.SetBool("play", true);  //予兆アニメーション開始
            if (countOmen >= omenTime)       //予兆終了
            {
                waveObject.SetActive(false); //波エフェクト非表示
                animator.SetBool("stop", true);  //予兆アニメーションストップ
                animator.SetBool("play", false); //予兆アニメーションリセット
                countOmen = 0;      //予兆カウントリセット
                isStop = false;    //設定終了
                isAttack = true;   //攻撃開始
            }
        }
    }

    public void StartAoe()
    {
        OmenSpawn();
        isOmen = true;
    }

    void OmenSpawn()
    {
        omen.transform.position = spawnPoint.position + new Vector3(0, spawnPoint.localScale.y, 0);
        omenObject.SetActive(true);
    }
    void WaveMove()
    {
        Vector3 move = waveObject.transform.position;
        move.x += Mathf.Repeat(moveSpeed * Time.deltaTime, 360.0f);
        waveObject.transform.position = move;
    }
    private void WaveSpawn()
    {
        waveObject.SetActive(true);
        Vector3 move = waveObject.transform.position;
        waveObject.transform.position = new Vector3(move.x , 0.0f , move.z);
        omenObject.SetActive(false);
    }

    public void SetAoe(float omenT, float waveT, float delayT, float speed)
    {
        omenTime = omenT;
        waveTime = waveT;
        delayTime = delayT;
        moveSpeed = speed;
    }
    public bool IsAttack() { return isAttack; }
    public bool IsOmen() { return isOmen; }
    public bool IsDelay() { return isDelay; }
    public bool GetIsStop() { return isStop; }

    public void SetStop()
    {
        isStop = false;
    }
}

