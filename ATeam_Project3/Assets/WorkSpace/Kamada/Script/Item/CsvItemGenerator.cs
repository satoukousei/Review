using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CsvItemGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemPrefabs;
    [SerializeField]
    private Transform[] spawn;
    [SerializeField]
    private FadeCreate fade;

    private int count = 0;
    private float countCool = 0;

    public List<ItemData> itemData = new List<ItemData>();
    public struct ItemData
    {
        public int itemNumber;
        public int posNumber;
        public float coolTime;
        public float lifeTime;
    }
    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Item.csv");
        var lines = File.ReadAllLines(path, Encoding.UTF8);
        for (int i = 1; i < lines.Length; i++) // ヘッダーはスキップ
        {
            if (lines[i].StartsWith("END") || lines[i].StartsWith("end"))
            {
                break;
            }

            var values = lines[i].Split(',');

            itemData.Add(new ItemData
            {
                itemNumber = int.Parse(values[0]),       //アイテム番号
                posNumber = int.Parse(values[1]),        //出現位置番号
                coolTime = float.Parse(values[2]),       //クールタイム
                lifeTime = float.Parse(values[3]),       //表示時間
            });
        }
    }

    private void Update()
    {
        if (!fade.IsStartFadeEnd)
        {
            return;
        }

        if (count < itemData.Count && countCool >= itemData[count].coolTime)
        {
            SpawnItem();
        }
        else
        {
            countCool += Time.deltaTime;
        }
    }

    private void SpawnItem()
    {
        var data = itemData[count];
        var obj = Instantiate(itemPrefabs[data.itemNumber], spawn[data.posNumber].position, Quaternion.identity);
        obj.SetActive(true);
        var item = obj.GetComponent<SetItemCount>();
        if(item != null)
        {
            item.SetItem(data.lifeTime);
        }

        countCool = 0;
        count++;

        if (count >= itemData.Count)
        {
            count = 0;
        }
    }
}