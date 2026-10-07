using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class FallGround : MonoBehaviour
{
    [SerializeField]
    private GameObject[] groundSpawnPos;
    [SerializeField]
    private GameObject omen;
    [SerializeField]
    private GameObject omenObject;

    private bool isEnd = false;

    private float times = 0;
    private int count = 0;



    public List<float[]> GroundData { get; private set; } = new List<float[]>();

    private List<float[]> TimeData = new List<float[]>();
    private void Start()
    {
        for (int i = 0; i < groundSpawnPos.Length; i++)
        {
            groundSpawnPos[i].SetActive(true);
        }

        string path = Path.Combine(Application.streamingAssetsPath, "GroundHide.csv");
        var lines = File.ReadAllLines(path, Encoding.UTF8);
        for (int i = 1; i < lines.Length; i++) // ヘッダーはスキップ
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            var values = lines[i].Split(',');

   
           float timing = float.Parse(values[0]);// 出現タイミング
           float position = float.Parse(values[1]);// 出現位置
           float omenTime = float.Parse(values[2]);// 予兆時間
           float fallTime = float.Parse(values[3]);// 落下時間

            GroundData.Add(new float[] { timing, position, omenTime, fallTime });
            TimeData.Add(new float[] { 0,0,0});
        }
    }
    private void Update()
    {
        if (!isEnd)
        {
            if(count >= GroundData.Count)
            {
                isEnd = true;
                return;
            }

            times += Time.deltaTime;
            if (times >= GroundData[count][0])
            {
                GameObject omenObject = Instantiate(omen, groundSpawnPos[(int)GroundData[count][1]].transform.position, Quaternion.identity);
                times = 0;

                if (count < GroundData.Count)
                {
                    ++count;
                }
            }
        }

        for(int i = 0; i < count; i++)
        {
            Debug.Log("0");
            TimeData[i][0] += Time.deltaTime;
            if (TimeData[i][0] >= GroundData[i][2])
            {
                Debug.Log("1");
                if (TimeData[i][2] <= 0)
                {
                    Debug.Log("2");
                    Destroy(omenObject);
                    groundSpawnPos[(int)GroundData[i][1]].SetActive(false);   
                    TimeData[i][2] = 1;
                }
                else
                {
                    Debug.Log("3");
                    TimeData[i][1] += Time.deltaTime;
                    if (TimeData[i][1] >= GroundData[i][3])
                    {
                        groundSpawnPos[(int)GroundData[i][1]].SetActive(true);
                    }
                }
            }
        } 
    }

    //private IEnumerator groundHide()
    //{
    //    times = 0;
      
    //    yield return new WaitForSeconds(GroundData[count][2]);//予兆の時間待機
    //    Destroy(omenObject);
    //    groundSpawnPos[(int)GroundData[count][1]].SetActive(false);


    //    yield return new WaitForSeconds(GroundData[count][3]);//落下してから復活するまでの時間待機
    //    groundSpawnPos[(int)GroundData[count][1]].SetActive(true);
    //}
}
