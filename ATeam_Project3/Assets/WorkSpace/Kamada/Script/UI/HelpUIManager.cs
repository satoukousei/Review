using UnityEngine;

public class HelpUIManager : MonoBehaviour
{
    [SerializeField, Header("ヘルプアニメーション")]
    private HelpUIAnimation helpUiAnim = null;
    [SerializeField, Header("ヘルプフェードスクリプト")]
    private HeartFade helpFade = null;

    private void Start()
    {
        helpFade.FadeOut();
        helpUiAnim.StartAnimation();
    }
}
