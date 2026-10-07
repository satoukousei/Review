using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField] private EffectManager effectManager = null;
    [SerializeField] private SEManager seManager = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("StopBullet"))
        {
            DestroyObject();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            DestroyObject();
            seManager.PlayPlayerDebuffSE();
            seManager.PlayBossAoeSE();
        }
    }
    private void DestroyObject()
    {
        effectManager.CreateEffect(2, transform.position, 2);
        Destroy(gameObject);
    }
}
