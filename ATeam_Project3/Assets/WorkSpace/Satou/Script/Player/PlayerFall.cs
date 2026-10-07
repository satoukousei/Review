using UnityEngine;

public class PlayerFall : MonoBehaviour
{

    [SerializeField]
    private Transform playerPos;

    [SerializeField]
    private Collider playerCollider;

    [SerializeField]
    private float fallLimitTime = 0.0f;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private ParticleSystem effect;

    [SerializeField]
    private PlayerPull playerPull;

    [SerializeField]
    private string fallWallTag = "FallWall";

    [SerializeField]
    private PlayerDamage playerDamage;

    private float countTime = 0.0f;

    private bool timerFlag = false;
    private bool isFall    = false; //落下中かどうかのフラグ
    public bool isFalling = false; //落下中のフラグ

    private void Start()
    {
        effect.Stop();
    }

    private void Update()
    {
        if(playerPull.GetIsPulling())
        {
            effect.Clear();
            effect.Stop();
            timerFlag = false;
            countTime = 0;
        }
        if (timerFlag)
        {
            countTime += Time.deltaTime;

            if (countTime >= fallLimitTime)
            {
                effect.Stop();

                rb.useGravity = true;
                playerCollider.isTrigger = true;
                isFalling = true;
                countTime = 0;
                timerFlag = false;
                isFall = false;
            }
        }
        else
        {
            countTime = 0;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag(fallWallTag)&& !isFall)
        { 
            effect.Play();
            rb.useGravity = false; //重力無効
            rb.linearVelocity = Vector3.zero;
            timerFlag = true;
            isFall = true;
        }
        if(collider.gameObject.CompareTag("Player"))
        {
            effect.Clear();
            effect.Stop();
            rb.useGravity = true;
            playerCollider.isTrigger = false;
            isFall = false;
            timerFlag = false;
            countTime = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(fallWallTag))
        {
            effect.Clear();
            effect.Stop();
            timerFlag = false;
            countTime = 0;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            effect.Clear();
            effect.Stop();
            timerFlag = false;
            countTime = 0;
        }
    }


    public bool GetisFallFlag()
    {
        return isFall;
    }
    public bool GetIsFall()
    {
        return isFalling;
    }

}
