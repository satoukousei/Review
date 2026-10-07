using UnityEngine;

public class TestStun : MonoBehaviour
{
    [SerializeField] private float limitStunTime = 1.0f;
    [SerializeField] private float limitInvincibleTime = 2.0f;
    [SerializeField] private float renderLimitTime = 0.2f;

    [SerializeField] private PlayerDamage playerDamage;
    [SerializeField] private Renderer render;

    private float stunTimer = 0f;
    private float invincibleTimer = 0f;
    private float renderTimer = 0f;

    private bool isStun = false;
    private bool isInvincible = false;
    private bool wasDamage = false;

    void Update()
    {
        bool isNowDamage = playerDamage.GetIsDamage();

        // ダメージを受けた瞬間だけスタン開始
        if (isNowDamage && !wasDamage)
        {
            StartStun();
        }

        wasDamage = isNowDamage;

        UpdateStun();
        UpdateInvincible();
    }

    private void StartStun()
    {
        isStun = true;
        stunTimer = 0f;
    }

    private void UpdateStun()
    {
        if (!isStun) return;

        stunTimer += Time.deltaTime;

        if (stunTimer >= limitStunTime)
        {
            isStun = false;
            StartInvincible();
        }
    }

    private void StartInvincible()
    {
        isInvincible = true;
        invincibleTimer = 0f;
    }

    private void UpdateInvincible()
    {
        if (!isInvincible) return;

        invincibleTimer += Time.deltaTime;
        renderTimer += Time.deltaTime;

        // 点滅処理
        if (renderTimer >= renderLimitTime)
        {
            render.enabled = !render.enabled;
            renderTimer = 0f;

            // 無敵終了
            if (invincibleTimer >= limitInvincibleTime)
            {
                isInvincible = false;
                playerDamage.ResetIsDamage();
                render.enabled = true;
            }
        }
    }

    public bool IsStun()
    {
        return isStun;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}
