using UnityEngine;

public class TrakingObjCollision : MonoBehaviour
{
    [SerializeField]
    private PlayerTracking track;
    [SerializeField]
    private SEManager seManager;
    private PlayerMove player = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerMove>();
            player.SetIsSlow(true);
            seManager.PlayPlayerDebuffSE();
            track.DestroyTrackObject();
        }
    }
}
