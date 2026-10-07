using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class PlayerTrakingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject trakingObject = null;
    [SerializeField]
    private GameObject[] spawnPoint;
    [SerializeField]
    private ParticleSystem[] spawnEffect = null;

    private int count = 0;
    private float countCool = 0;
    private bool isPlay = false;

    public List<TrackData> trackData = new List<TrackData>();
    public struct TrackData
    {
        public int posNumber;
        public float coolTime;
        public float speed;
        public float lifeTime;
    }
    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "PlayerTracking.csv");
        var lines = File.ReadAllLines(path, Encoding.UTF8);
        for (int i = 1; i < lines.Length; i++) // ヘッダーはスキップ
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            var values = lines[i].Split(',');

            trackData.Add(new TrackData
            {
                coolTime = float.Parse(values[0]),   //クールタイム
                posNumber = int.Parse(values[1]),    //出現位置番号
                speed = float.Parse(values[2]),      //速度
                lifeTime = float.Parse(values[3])    //出現時間
            });
        }
    }

    private void Update()
    {
        if (count < trackData.Count && countCool >= trackData[count].coolTime)
        {
            if (!isPlay)
            {
                StartCoroutine(StarTtrck(1));
                isPlay = true;
            }
        }
        else
        {
            countCool += Time.deltaTime;
        }
    }
    private IEnumerator StarTtrck(float delay)
    {
        spawnEffect[trackData[count].posNumber].Play();
        yield return new WaitForSeconds(delay);
        SpawnTrackObject();
    }

    private void SpawnTrackObject()
    {
        var data = trackData[count];
        var obj = Instantiate(trakingObject, spawnPoint[data.posNumber].transform.position, Quaternion.identity);
        obj.SetActive(true);

        var track = obj.GetComponent<PlayerTracking>();
        track.SetTrackData(data.speed,data.lifeTime);
        track.StartTraking();

        countCool = 0;
        count++;
        isPlay = false;

        if (count >= trackData.Count)
        {
            count = 0;
        }
    }
}
