using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class FadeCraft : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stage;
    private int count = 0;
    private float fadeTime = 0;
    private int maxCount = 0;  
    public List<float[]> AoeData { get; private set; } = new List<float[]>();
    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "AOE_2.csv");
        var lines = File.ReadAllLines(path, Encoding.UTF8);
        maxCount=lines.Length -1;
        for (int i = 1; i < lines.Length; i++) // ヘッダーはスキップ
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            var values = lines[i].Split(',');

            AoeData.Add(new float[]
            {
                float.Parse(values[0]),// 出現タイミング
                float.Parse(values[1]),// fade時間
                float.Parse(values[2]),//ステ1
                float.Parse(values[3]),//ステ2
                float.Parse(values[4]),//ステ3
                float.Parse(values[5]),//ステ4
                float.Parse(values[6]),//ステ5
                float.Parse(values[7]),//ステ6
                float.Parse(values[8]),//ステ7
                float.Parse(values[9]),//ステ8
            });
        }
    }

    void Update()
    {
        fadeTime += Time.deltaTime;
        if (maxCount > count&&AoeData[count][0]<=fadeTime)
        {
            for(int i=0;i<stage.Length; i++)
            {
                if (AoeData[count][i +2] == 1)
                {
                    Debug.Log("FadeOut");
                    stage[i].GetComponent<FadeMaterial>().OnFadeOut(AoeData[count][1]);
                }
                else
                {
                    continue;
                }
            }
            count++; 

            fadeTime = 0;
        }
    }
}
