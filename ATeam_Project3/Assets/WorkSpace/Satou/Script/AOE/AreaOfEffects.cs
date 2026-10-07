using UnityEngine;
using UnityEngine.InputSystem;

public class AreaOfEffects : MonoBehaviour
{
    [SerializeField]
     private GameObject Player;
    [SerializeField]
    private GameObject area;
    [SerializeField]
    private float coolTime = 0.0f;
    
    private float attackTime = 0.0f;
    private bool isAttack = false;
    private bool attack = false;
    void Update()
    {
        if (!attack && !isAttack)
        {
            coolTime += 1.0f * Time.deltaTime;

            if (coolTime > 3.0f)
            {
                isAttack = true;
                coolTime = 0.0f;
            }
        }

        if (isAttack)
        {
            attackTime += 1.0f * Time.deltaTime;
            area.transform.position = Player.transform.position - new Vector3(0.0f, 0.1f, 0.0f);
            transform.position = area.transform.position + new Vector3(0.0f, 25.0f, 0.0f);

            if (attackTime > 1.0f)
            {
                isAttack = false;
                attack = true;
                attackTime = 0.0f;
            }
        }
        else
        {
            if (!attack)
            {
                transform.position = new Vector3(0.0f, 100.0f, 0.0f);
                area.transform.position = new Vector3(0.0f, 100f, 0.0f);
            }
        }

        if (attack)
        {
            transform.position += new Vector3(0.0f, -5.0f, 0.0f) * Time.deltaTime;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isAttack = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            attack = false;
        }
    }
}