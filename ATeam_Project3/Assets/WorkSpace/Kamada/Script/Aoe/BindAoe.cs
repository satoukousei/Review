using UnityEngine;
using UnityEngine.InputSystem;

public class BindAoe : MonoBehaviour,IAoe
{
    [SerializeField]
    private Transform[] player = null;
    [SerializeField]
    private PlayerMove[] playerMove = null;
    [SerializeField]
    private GameObject aoeObject = null;
    [SerializeField]
    private GameObject omenObject = null;

    private float rotateSpeed = 0;
    private float scaleDownSpeed = 0;
    private int playerNumber = 0;

    private float attackTime = 0;
    private float delayTime = 0;

    private bool isStart = false;
    private bool isAttack = false;
    private bool isStop = false;

    private float countDelay = 0;
    private float countAttack = 0;
    void Awake()
    {
        aoeObject.SetActive(false);
        omenObject.SetActive(false);
    }

    void Update()
    {
        if (isStart)
        {
            Delay();
        }

        if(isAttack)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (isAttack)
        {
            Rotate();
        }
    }

    private void Delay()
    {
        if (!omenObject.activeSelf)
        {
            omenObject.SetActive(true);
            aoeObject.SetActive(true);
        }

        aoeObject.transform.position = player[playerNumber].position;
        countDelay += Time.unscaledDeltaTime;
        if (countDelay >= delayTime)
        {
            playerMove[playerNumber].SetStunFlag(true);
            omenObject.SetActive(false);
            isStart = false;
            isAttack = true;
            countDelay = 0;
        }
    }

    private void Rotate()
    {
        aoeObject.transform.eulerAngles += new Vector3(0, rotateSpeed, 0) * Time.unscaledDeltaTime;
        if (aoeObject.transform.localScale.x >= 0.2)
        {
            aoeObject.transform.localScale -= new Vector3(scaleDownSpeed, 0, scaleDownSpeed) * Time.unscaledDeltaTime;
        }
    }
    private void Attack()
    {
        if (!aoeObject.activeSelf)
        {
            aoeObject.SetActive(true);
        }

        countAttack += Time.unscaledDeltaTime;
        if (countAttack >= attackTime ||aoeObject.transform.localScale.x <= 0.2)
        {
            isAttack = false;
            isStop = true;
            countAttack = 0;
            aoeObject.transform.eulerAngles = new Vector3(0, 0, 0);
            aoeObject.transform.localScale = new Vector3(1, 1, 1);
            aoeObject.SetActive(false);
            playerMove[playerNumber].SetStunFlag(false);
        }
    }

    public void SetAoe(float _attackTime, float _delayTime,float mini,float scale,int player)
    {
        attackTime = _attackTime;
        delayTime = _delayTime;
        rotateSpeed = mini;
        scaleDownSpeed = scale / 100;
        playerNumber = player;
    }

    public void AoeStart()
    {
        isStart = true;
        aoeObject.SetActive(true);
        omenObject.SetActive(true);
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