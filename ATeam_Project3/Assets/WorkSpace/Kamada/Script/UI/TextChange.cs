using UnityEngine;

public class TextChange : MonoBehaviour
{
    [Header("テキストの最大サイズ")]
    [SerializeField]
    private float MaxTextSize = 0;
    [Header("テキストの最小サイズ")]
    [SerializeField]
    private float MinTextSize = 0;
    [Header("リトライテキスト")]
    [SerializeField]
    private GameObject retryTextObject;
    [Header("タイトルテキスト")]
    [SerializeField]
    private GameObject titleTextObject;

    private bool TextFlag = true;
    private float floatY = 0;

    void Update()
    {
        TextChanger();
        floatY = Input.GetAxisRaw("Vertical");//縦の入力を取得

        if (floatY > 0)
        {
            TextFlag = true;
        }
        else if (floatY < 0)
        {
            TextFlag = false;
        }
    }

    private void TextChanger()
    {
        if (TextFlag)
        {
            retryTextObject.transform.localScale = new Vector3(MaxTextSize, MaxTextSize, MaxTextSize);
            titleTextObject.transform.localScale = new Vector3(MinTextSize, MinTextSize, MinTextSize);
        }
        else
        {
            retryTextObject.transform.localScale = new Vector3(MinTextSize, MinTextSize, MinTextSize);
            titleTextObject.transform.localScale = new Vector3(MaxTextSize, MaxTextSize, MaxTextSize);
        }
    }
    public bool GetTextFlag()
    {
        return TextFlag;
    }
}