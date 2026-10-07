using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Laserspawn : MonoBehaviour
{

    [SerializeField]
    private GameObject[] laserPrefab;
    [SerializeField]
    private GameObject[] laserOmen;
    [SerializeField]
    private GameObject[] spawnpos;

    private float times = 0;
    private int count = 0;
    private int maxCount = 0;

    public List<float[]> AoeData { get; private set; } = new List<float[]>();
    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "AOE_3.csv");
        var lines = File.ReadAllLines(path, Encoding.UTF8);
        maxCount = lines.Length - 1;
        for (int i = 1; i < lines.Length; i++) // ヘッダーはスキップ
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            var values = lines[i].Split(',');

            AoeData.Add(new float[]
            {
                float.Parse(values[0]),// 出現タイミング
                float.Parse(values[1]),// Aoeの種類
                float.Parse(values[2]),// fadetime1
                float.Parse(values[3]),// fadetime2
                float.Parse(values[4]),// spawnpos1
                float.Parse(values[5]),// spawnpos2
                float.Parse(values[6]),// rollspeed1
                float.Parse(values[7]),// rollspeed2
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
 

        times += Time.deltaTime;

        if (AoeData[(int)count][0] <= times && AoeData[(int)count][0] + 1.0f > times)
        {
              StartCoroutine(Laser((int)count));

            times = 0;
            if (count < AoeData.Count - 1)
            {
                ++count;
            }
            else
            {
                count = 0;
            }
        }
    }
    public IEnumerator Laser(int index)//LaserOmenの生成と破壊した後にLaserAoeの生成と破壊
    {
        GameObject omen = Instantiate(laserOmen[0], spawnpos[(int)AoeData[index][3]].transform.position, Quaternion.identity);//LaserOmenの生成
        yield return new WaitForSeconds(AoeData[index][1]);
        Destroy(omen);//LaserAoeの破壊

        GameObject laser = Instantiate(laserPrefab[(int)AoeData[index][1]], spawnpos[(int)AoeData[index][4]].transform.position,Quaternion.identity);
        yield return new WaitForSeconds(AoeData[index][2]);
    }
}

