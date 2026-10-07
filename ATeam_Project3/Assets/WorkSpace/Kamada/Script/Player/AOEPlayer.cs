using UnityEngine;
using TMPro;

public class AOEPlayer : MonoBehaviour
{
    [SerializeField]
    private int playerHp = 0;
    [SerializeField]
    private TextMeshProUGUI playerHpText;
    void Start()
    {
        playerHpText.text = playerHp.ToString();
    }

    /// <summary>
    /// プレイヤーのHPをマイナスする処理
    /// </summary>
    /// <param name="minusHp">減らすHPを指定</param>
    /// <param name="playerFlag">プレイヤー1かプレイヤー2をフラグで判定</param>
    public void MinusHp(int minusHp)
    {
       playerHp -= minusHp;
       playerHpText.text = playerHp.ToString();
    }

    public void PlusHp(int plusHp)
    {
        playerHp += plusHp;
        playerHpText.text = playerHp.ToString();
    }
}
