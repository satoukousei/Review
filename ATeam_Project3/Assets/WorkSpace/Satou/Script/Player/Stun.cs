using UnityEngine;
using UnityEngine.Rendering;

public class Stun : MonoBehaviour
{
    [SerializeField]
    private Knockback knockback;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private PlayerMove playerMove;


    private bool isStun = false;

    public void OnCollisionStay(Collision collision)
    {

        if (knockback.GetIsKnockBack() && collision.gameObject.CompareTag("FallWall"))
        {
            isStun = true;     
            playerMove.SetStunFlag(true);
        }
    }


    public bool GetIsStun()
    {
        return isStun;
    }
    public void ResetIsStun()
    {
        isStun = false;
        knockback.ResetKnockback();
        playerMove.SetStunFlag(false);
    }

}
