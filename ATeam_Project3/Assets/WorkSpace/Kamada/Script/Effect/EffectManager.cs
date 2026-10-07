using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] particle;
    public void CreateEffect(int number,Vector3 pos,float size)
    {
        GameObject particleSystem = Instantiate(particle[number], pos, Quaternion.identity);
        particleSystem.transform.localScale *= size;
    }
}
