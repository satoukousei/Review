using System.Collections;
using UnityEngine;

public class RouletteAoe : MonoBehaviour, IAoe
{
    [Header("オブジェクト設定")]
    [SerializeField] private GameObject aoeOmen = null;
    [SerializeField] private Renderer rend = null;
    [SerializeField] private Transform[] spawnPoint = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private EffectManager effectManager = null;
    [SerializeField] private GameObject hand = null;
    [SerializeField] private CameraShake cameraShake = null;
    [Header("ゆらゆら演出設定")]
    [SerializeField] private float wobbleX = 0.3f;
    [SerializeField] private float wobbleZ = 0.3f;
    [SerializeField] private float wobbleSpeed = 3f;
    [SerializeField] private SEManager seManager = null;

    private Vector3 logicalPos;

    private float rouletteTime = 0;
    private float attackTime = 0;
    private float delayTime = 0;
    private float countRoulette = 0;
    private float countDelay = 0;
    private float countAttack = 0;
    private float moveSpeed = 0;
    private float moveDelay = 0;
    private float wobbleTime = 0f;

    private int spawnNumber = 0;
    private int stopIndex = -1;

    private bool isStart = false;
    private bool isDelay = false;
    private bool isAttack = false;
    private bool isStop = false;
    private bool isMovement = false;
    private bool isOverTime = false;
    private void Awake()
    {
        aoeOmen.SetActive(false);
        hand.SetActive(false);
    }
    private void Update()
    {
        if (isStart)
        {
            Starting();
        }

        if (isDelay)
        {
            Delay();
        }

        if (isAttack)
        {
            Attacking();
        }
    }

    private void LateUpdate()
    {
        if (!aoeOmen.activeSelf)
        {
            return;
        }

        wobbleTime += Time.unscaledDeltaTime;

        float t = wobbleTime * wobbleSpeed;
        float x = Mathf.Sin(t) * wobbleX;
        float z = Mathf.Sin(t * 2f) * wobbleZ;

        Vector3 wobble = new Vector3(x, 0f, z);

        if (isOverTime)
        {
            wobble *= 0.3f;
        }

        aoeOmen.transform.position = logicalPos + wobble;
        hand.transform.position = logicalPos + wobble + new Vector3(0f, 30f, 0f);
    }

    private void Starting()
    {
        if (!aoeOmen.activeSelf)
        {
            aoeOmen.SetActive(true);
            hand.SetActive(true);
            logicalPos = aoeOmen.transform.position;
            wobbleTime = 0f;
        }

        countRoulette += Time.unscaledDeltaTime;

        if (countRoulette >= rouletteTime)
        {
            isOverTime = true;

            logicalPos = Vector3.MoveTowards(
                logicalPos,
                spawnPoint[stopIndex].position,
                moveSpeed * Time.unscaledDeltaTime
            );

            float distance = Vector3.Distance(logicalPos, spawnPoint[stopIndex].position);

            if (distance <= 0.01f)
            {
                countRoulette = 0;
                rend.material.color = Color.red;
                animator.SetBool("play", true);

                isStart = false;
                isDelay = true;
                isMovement = false;
                isOverTime = false;
            }

            return;
        }

        Roulette();
    }
    private void Delay()
    {
        countDelay += Time.unscaledDeltaTime;

        if (countDelay >= delayTime)
        {
            aoeOmen.SetActive(false);
            animator.SetBool("play", false);

            countDelay = 0;
            isDelay = false;
            isAttack = true;

            seManager.PlayBossHandFallSE();
        }
    }

    private void Attacking()
    {
        Attack();

        countAttack += Time.unscaledDeltaTime;

        if (countAttack >= attackTime)
        {
            seManager.PlayBossHandAttackSE();
            StartCoroutine(AttackStart(0.5f));
            effectManager.CreateEffect(3, aoeOmen.transform.position, 1);
            StartCoroutine(cameraShake.Shake(0.5f, 0.4f));

            countAttack = 0;
            isAttack = false;
        }
    }

    private void Attack()
    {
        if (attackTime <= 0f)
        {
            hand.transform.position = aoeOmen.transform.position;
            return;
        }

        float t = Mathf.Clamp01(countAttack / attackTime);

        hand.transform.position = Vector3.Lerp(
            aoeOmen.transform.position + new Vector3(0f, 30f, 0f),
            aoeOmen.transform.position,
            t
        );
    }

    private IEnumerator AttackStart(float time)
    {
        yield return new WaitForSecondsRealtime(time * 3);
        hand.SetActive(false);
        isStop = true;
    }
    private void Roulette()
    {
        if (!isMovement)
        {
            spawnNumber = spawnNumber == 0 ? 1 : 0;
            isMovement = true;
        }
        else
        {
            MoveRoulette(spawnNumber);
        }
    }

    private void MoveRoulette(int number)
    {
        logicalPos = Vector3.MoveTowards(
            logicalPos,
            spawnPoint[number].position,
            moveSpeed * Time.unscaledDeltaTime
        );

        float distance = Vector3.Distance(logicalPos, spawnPoint[number].position);

        if (distance <= 0.01f)
        {
            moveDelay += Time.unscaledDeltaTime;

            if (moveDelay >= 1.0f)
            {
                moveDelay = 0;
                isMovement = false;
            }
        }
    }

    public void SetRoulette(float omen, float delay_, float aoe, float speed, float stopPos)
    {
        rouletteTime = omen;
        delayTime = delay_;
        attackTime = aoe;
        moveSpeed = speed;
        stopIndex = (int)stopPos;
    }
    public void RouletteStart()
    {
        isStart = true;
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