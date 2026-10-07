using UnityEngine;

public class PlayerCatch : MonoBehaviour
{
    private float countCatchTime = 0;//キャッチの許容時間カウント
    private float countCoolTime = 0;//クールタイムカウント
    private float storageCatchTime = 0;
    private float storageCoolTime = 0;
    private bool isCatch = false;//キャッチしているかどうかのフラグ
    private bool canCatch = true;//キャッチ可能かどうかのフラグ

    public void SetTime(float catchTime, float countTime)
    {
        countCatchTime = catchTime;
        countCoolTime = countTime;
        storageCatchTime = catchTime;
        storageCoolTime = countTime;
    }

    public void Catch()
    {
        if (isCatch)//キャッチしているなら
        {
            countCatchTime -= Time.deltaTime;
            if (countCatchTime <= 0.0f)
            {
                isCatch = false;
                countCatchTime = storageCatchTime;
            }
        }
        else
        {
            if (!canCatch)
            {
                countCoolTime -= Time.deltaTime;
                if (countCoolTime <= 0.0f)
                {
                    canCatch = true;
                    countCoolTime = storageCoolTime;
                }
            }
        }
    }
    public void CatchStart()
    {
        isCatch = true;
        canCatch = false;
    }

    public bool IsCatch() { return isCatch; }
    public bool CanCatch() { return canCatch; }
}
