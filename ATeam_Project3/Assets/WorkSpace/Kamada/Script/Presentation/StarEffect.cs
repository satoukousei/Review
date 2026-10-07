using UnityEngine;

public class StarEffect : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private Vector3 offset = Vector3.zero;
    void Update()
    {
        transform.position = player.position + offset;
    }
}
