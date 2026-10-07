using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    [SerializeField]
    private TutorialItemCount itemCount; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            itemCount.PlusCount();
        }
    }
}