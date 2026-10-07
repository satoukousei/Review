using UnityEngine;

public class ItemTracking : MonoBehaviour
{
    [Header("アイテム")]
    [SerializeField] private GameObject[] items = null;
    [Header("追従先のオブジェクト")]
    [SerializeField] private Transform player = null;
    [Header("アイテムマネージャー")]
    [SerializeField] private GetItemManager getItemManager = null;
    [Header("追従の幅")]
    [SerializeField] private float followDistance = 0;
    [Header("スプリングの強さ")]
    [SerializeField] private float springStrength = 0;
    [Header("減衰")]
    [SerializeField] private float damping = 0;
    [Header("プレイヤー0or1")]
    [SerializeField] private int number = 0;

    private Vector3[] velocities;
    private bool isFollowing = true;

    private int currentCount = 0;

    private void Awake()
    {
        velocities = new Vector3[items.Length];

        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (player == null || !isFollowing) return;

        currentCount = Mathf.Clamp(
            getItemManager.GetPoint(number),
            0,
            items.Length
        );

        for (int i = 0; i < currentCount; i++)
        {
            Transform target = (i == 0) ? player : items[i - 1].transform;
            MoveItem(target, i);
        }

        for (int i = currentCount; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }
    }

    private void MoveItem(Transform target, int index)
    {
        Vector3 targetPosition =
            target.position - (target.forward * followDistance);

        Vector3 displacement =
            targetPosition - items[index].transform.position;

        Vector3 springForce = displacement * springStrength;
        Vector3 dampingForce = -velocities[index] * damping;

        Vector3 force = springForce + dampingForce;

        velocities[index] += force * Time.deltaTime;
        items[index].transform.position +=
            velocities[index] * Time.deltaTime;

        items[index].transform.LookAt(target.position);
    }
    public void SetItemPos(Vector3 spawnPos)
    {
        int spawnIndex = getItemManager.GetPoint(number) - 1;

        if (spawnIndex < 0 || spawnIndex >= items.Length) return;

        items[spawnIndex].SetActive(true);
        items[spawnIndex].transform.position = spawnPos;

        velocities[spawnIndex] = Vector3.zero; // 初速リセット
    }

    public void ResetItem(int index)
    {
        if (index < 0 || index >= items.Length) return;

        items[index].SetActive(false);
        velocities[index] = Vector3.zero;
    }

    public void SetFollowing(bool flag)
    {
        isFollowing = flag;
    }
}