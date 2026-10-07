using UnityEngine;
using System.Collections.Generic;

public class ItemObjectPool : MonoBehaviour
{
    public GameObject prefab;       // プールするオブジェクト
    public int poolSize = 10;       // プール数
    private List<GameObject> pool;  // プールリスト

    void Awake()
    {
        pool = new List<GameObject>();

        // あらかじめ生成して非表示に
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    // プールから取得
    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        // 全部使用中なら新しく生成（必要なら拡張）
        GameObject newObj = Instantiate(prefab, position, rotation);
        pool.Add(newObj);
        return newObj;
    }

    // プールに返却
    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
    }
}