using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class AoeCsv : MonoBehaviour
{
    [Header("AOEプレハブ")]
    [SerializeField]
    private GameObject[] aoePrefab;
    [Header("AOE予兆プレハブ")]
    [SerializeField]
    private GameObject[] aoeOmen;
    [Header("AOE出現位置")]
    [SerializeField]
    private GameObject[] spawnPosition;

    private float timing = 0;
    private float aoe = 0;
    private float aoeindex = 0;
    private float aoeCountTimes = 0;
    private float aoeDelayTime = 0;
    private float count = 0;
    private float position = 0;
    private float times = 0;

    public List<float[]> AoeData   { get; private set; } = new List<float[]>();
    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "AOE.csv");
        var lines = File.ReadAllLines(path, Encoding.UTF8);
        for (int i = 1; i < lines.Length; i++) // ヘッダーはスキップ
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            var values = lines[i].Split(',');

            timing        = float.Parse(values[0]);// 出現タイミング
            aoe           = float.Parse(values[1]);// aoeの数
            aoeindex      = float.Parse(values[2]);// aoeの種類
            aoeCountTimes = float.Parse(values[3]);// aoeの継続時間
            aoeDelayTime  = float.Parse(values[4]);// aoeの予兆時間
            position      = float.Parse(values[5]);// aoeの出現位置


            AoeData.Add(new float[] { timing, aoe, aoeindex, aoeCountTimes, aoeDelayTime, position });

        }
    }

    private void Update()
    {


        //timingの計算
        times += Time.deltaTime;

        if (AoeData[(int)count][0] <= times && AoeData[(int)count][0] + 1.0f > times)
        {
            //StartCoroutine(AoeSpawns((int)count));

            times = 0;
            if(count < AoeData.Count - 1)
            {
                ++count;
            }
            else
            {
                count = 0;
            }
        }
    }

    //public IEnumerator AoeSpawns(int index) //AOEの生成と破壊
    //{
    //    GameObject obj = Instantiate(aoeOmen[0], spawnPosition[(int)AoeData[index][5]].
    //                                 transform.position, Quaternion.identity);//予兆の生成
    //    yield return new WaitForSeconds(AoeData[index][4]);//予兆の時間待機
    //    Destroy(obj);//予兆の破壊

    //    GameObject aoeObj = Instantiate(aoePrefab[(int)AoeData[index][2]], spawnPosition[(int)AoeData[index][5]].
    //                                    transform.position, Quaternion.identity);//AOEの生成
    //    yield return new WaitForSeconds(AoeData[index][3]);//AOEの継続時間待機
    //    Destroy(aoeObj);//AOEの破壊
    //}


}