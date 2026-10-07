using UnityEngine;

public class Gage : MonoBehaviour
{
    [SerializeField, Header("MainMask")]
    private RectTransform mainHpMask = null;
    [SerializeField, Header("Main")]
    private RectTransform mainHpGage = null;

    [SerializeField, Header("減少スピード")]
    private float decreaseSpeed = 0.1f;
    [SerializeField, Header("ダメージ後減少するまでの一時停止時間")]
    private float waitTime = 0.5f;
    [SerializeField]
    private int fastAttackPlusDamage = 0;

    private int currentAttack = 0;
    private float maxGage = 100;
    private float nowGage = 0;
    private float time = 0f;
    private float minusGage = 0f;
    private bool isDecrease = false;

    private void Start()
    {
        nowGage = maxGage * ((mainHpGage.rect.width + mainHpGage.anchoredPosition.x) / mainHpMask.rect.width);
    }

    private void Update()
    {
        EffectGageUpdate();

    }

    private void EffectGageUpdate()
    {
        if (isDecrease)
        {
            if (time >= waitTime)
            {
                mainHpGage.anchoredPosition -= new Vector2(decreaseSpeed * Time.deltaTime, 0f);
                minusGage -= decreaseSpeed * Time.deltaTime;
                if (minusGage < 0)
                {
                    mainHpGage.anchoredPosition = new Vector2(-mainHpGage.rect.width * (1f - (nowGage / maxGage)),0f);
                    isDecrease = false;
                    time = 0f;
                    minusGage = 0f;
                }
            }
            else
            {
                time += Time.deltaTime;
            }

        }
    }
    public void GageUpdateDecrease(float minusGage)
    {
        if(nowGage == 0)
        {
            return;
        }
        float currentGage = maxGage - minusGage;
        nowGage -= minusGage;
        if (currentAttack < 2)
        {
            currentAttack++;
            currentGage -= fastAttackPlusDamage;
            nowGage -= fastAttackPlusDamage;
        }
        if (nowGage <= 0)
        {
            this.minusGage += mainHpGage.rect.width * (1f - ((maxGage + nowGage) / maxGage));
            nowGage = 0;
            return;
        }
        else
        {
            this.minusGage += mainHpGage.rect.width * (1f - (currentGage / maxGage));
        }
        isDecrease = true;
    }
    public float GetNowGage() { return nowGage; }
}
