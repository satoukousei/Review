using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    private ParticleSystem particle;
    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (particle.isStopped)
        {
            Destroy(this.gameObject);
        }
    }

}
