using UnityEngine;

public class Sweat : MonoBehaviour
{
    [SerializeField]
    private GameObject center=null;
    [SerializeField]
    private Vector3 localPos=Vector3.zero;

    private void Update()
    {
        this.transform.position = localPos + center.transform.position;

    }
}
