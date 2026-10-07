using UnityEngine;

public class GetItemManager : MonoBehaviour
{
    [SerializeField]
    private int maxPoint = 5;
    [SerializeField]
    private Gage gage;
    [SerializeField]
    private FlickeringUI flickeringUI;
    [SerializeField]
    private RandomPosMove randomPosMove;
    [SerializeField]
    private RectTransform gageImage;
    [SerializeField]
    private HeartMover heartMover;
    [SerializeField]
    private HeartGageUI heartGageUI;
    [SerializeField]
    private int minusPoint = 0;

    private int[] point = new int[2];
    private int division = 2;

    private void Start()//ポイントの初期化
    {
        for (int i = 0; i < point.Length; i++)
        {
            point[i] = 0;
        }
    }
    //ポイント加算の処理
    public void PointPlus(int number)
    {
        point[number]++;
    }
    public void BonusPointScore() //ポイントを倍にして加算する処理
    {
        for (int i = 0; i < heartMover.SuccessPlus.Length; i++)
        {
            heartMover.SuccessPlus[i] = null;
            heartMover.MissPlus[i] = null;
        }

        int total = point[0] + point[1];

        for(int i = 0; i < total; i++)
        {
            heartMover.SuccessPlus[i] = SuccessItemArrived;
        }

        heartMover.StartMove(total, true);

        // ポイント自体はここでリセット
        point[0] = 0;
        point[1] = 0;
    }
    public void DebuffPointScore()//加算する処理
    {
        for (int i = 0; i < heartMover.SuccessPlus.Length; i++)
        {
            heartMover.SuccessPlus[i] = null;
            heartMover.MissPlus[i] = null;
        }

        int total = (point[0] + point[1]);

        for (int i = 0; i < total; i++)
        {
            heartMover.MissPlus[i] = MissItemArrived;
        }

        heartMover.StartMove(total, false);

        // ポイント自体はここでリセット
        point[0] = 0;
        point[1] = 0;
    }
    //ポイント減少の処理
    public void PointMinus(int number)
    {
        point[number] /= division;
        gage.GageUpdateDecrease(minusPoint);
        flickeringUI.StartFlickering();
        randomPosMove.StartMove(gageImage.anchoredPosition);
    }
    public void MinusOnePoint(int number)
    {
        point[number]--;
        gage.GageUpdateDecrease(minusPoint);
        flickeringUI.StartFlickering();
        randomPosMove.StartMove(gageImage.anchoredPosition);
    }
    private void SuccessItemArrived()//アイテムがUIに到達したときの処理
    {
        heartGageUI.IsScoreSucces();
    }

    private void MissItemArrived()
    {
        heartGageUI.IsScoreFailed();
    }

    public int GetPoint(int number)
    {
        return point[number];
    }
    public int GetMaxPoint()
    {
        return maxPoint;
    }
}
