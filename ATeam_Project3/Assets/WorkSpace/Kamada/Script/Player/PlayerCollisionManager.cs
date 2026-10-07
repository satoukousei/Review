using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField]
    private PlayerPull pull = null;
    [SerializeField]
    private Collider playerCollider = null;

    void Update()
    {
        if (pull.GetIsBeingPulled())
        {
            playerCollider.isTrigger = true;
        }
        else
        {
              playerCollider.isTrigger = false;
        }
    }
}

