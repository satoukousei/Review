using UnityEngine;

public class ItemGenerate : MonoBehaviour
{
    [SerializeField] private ItemObjectPool itemObjPool;
    private float timer = 0;
    [SerializeField] private float count = 5;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= count)
        {
            SpawnRandom();
            timer = 0;
        }
    }
    private void SpawnRandom()
    {
        // Groundタグのオブジェクトをすべて取得
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");

        if (grounds.Length == 0) return;

        // ランダムに1つ選択
        GameObject randomGround = grounds[Random.Range(0, grounds.Length)];

        // Colliderがある場合はその範囲内でランダム位置を決定
        Vector3 spawnPos = randomGround.transform.position;
        Collider col = randomGround.GetComponent<Collider>();

        if (col != null)
        {
            Bounds bounds = col.bounds;
            spawnPos = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                0, // 地面の上に置く
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        // オブジェクト生成
        itemObjPool.Spawn(spawnPos,Quaternion.identity);
    }

}
