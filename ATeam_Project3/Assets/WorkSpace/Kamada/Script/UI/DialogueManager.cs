using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("会話データ")]
    [SerializeField] private string[] textData;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float typeSpeed = 0.05f;

    private bool isPlay = false;

    private void Start()
    {
        textComponent.text = "";
        ShowNextLine(0);
    }

    public void ShowNextLine(int number)
    {
        if (isPlay)
        {
            return;
        }
        isPlay = true;
        textComponent.text = "";
        StartCoroutine(TypeText(textData[number]));
    }

    private IEnumerator TypeText(string line)
    {
        for(int i = 0; i < line.Length; i++)
        {
            textComponent.text += line[i].ToString();
            yield return new WaitForSeconds(typeSpeed);

            if(i >= line.Length - 1)
            {
                isPlay = false;
            }
        }
    }
    public void TextReset()
    {
        textComponent.text = "";
    }

    public bool GetIsPlay() { return isPlay; }
}
