using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private GetItemManager itemManager;
    [SerializeField] private int number = 0;
    [SerializeField] private SEManager seManager;
    [SerializeField] private TestStun stun; // ← 追加
    [SerializeField] private ScreenFade redOut = null;
    [SerializeField] private HelpUIAnimation[] heartAnimation;
    [SerializeField] private GetItemManager getCount;
    [SerializeField] private int playerNumber = 0;
    [SerializeField] private float delayTime = 0;
    

    private float countDelay = 0;
    private int point = 0;
    private int loopCount = 0;
    private int currentLoopIndex = 0;

    private bool isPlayLoop = false;
    private bool isDamage = false;

    private void Start()
    {
        for (int i = 0; i < heartAnimation.Length; i++)
        {
            heartAnimation[i].IsDelete();
        }
    }
    private void Update()
    {
        for (int i = 0; i < heartAnimation.Length; i++)
        {
            if (heartAnimation[i].IsEnd())
            {
                heartAnimation[i].StopAnimation();
                heartAnimation[i].IsDelete();
            }
        }

        if (isPlayLoop)
        {
            countDelay += Time.deltaTime;

            if (countDelay >= delayTime)
            {
                countDelay = 0;

                int index = point - 1 - currentLoopIndex;

                if (index >= 0 && index < heartAnimation.Length)
                {
                    heartAnimation[index].IsInstantiate();
                    heartAnimation[index].StartAnimation();
                }

                currentLoopIndex++;

                if (currentLoopIndex >= loopCount)
                {
                    isPlayLoop = false;
                    currentLoopIndex = 0;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aoe"))
        {
            // スタン中 or 無敵中ならダメージ受けない
            if (stun.IsStun() || stun.IsInvincible())
            {
                return;
            }

            if (!isDamage)
            {
                point = getCount.GetPoint(playerNumber);
                loopCount = point - (point / 2);//破壊するハートの個数を取得する
                isPlayLoop = true;
                redOut.SetFadeImage();
                redOut.StartFadeOut();
                itemManager.PointMinus(number);
                seManager.PlayPlayerDamageSE();
                isDamage = true;
            }
        }

        if (other.CompareTag("Bullet"))
        {
            // スタン中 or 無敵中ならダメージ受けない
            if (stun.IsStun() || stun.IsInvincible())
            {
                return;
            }

            if (!isDamage)
            {
                point = getCount.GetPoint(playerNumber);
                loopCount = 1;
                isPlayLoop = true;
                redOut.SetFadeImage();
                redOut.StartFadeOut();
                itemManager.MinusOnePoint(number);
                seManager.PlayPlayerDamageSE();
                isDamage = true;
            }
        }
    }

    public bool GetIsDamage()
    {
        return isDamage;
    }

    public void ResetIsDamage()
    {
        isDamage = false;
    }
}
