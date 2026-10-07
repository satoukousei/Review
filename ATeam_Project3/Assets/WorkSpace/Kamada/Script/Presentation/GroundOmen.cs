using UnityEngine;

public class GroundOmen : MonoBehaviour
{
    [SerializeField]
    private ObjectHide objectHide;
    private Renderer rend;
    private float time = 0;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    private void Update()
    {
        if(objectHide.IsOmenPlay())
        {
            Omen();
        }
        else
        {
            rend.enabled = false;
        }
    }

    private void Omen()
    {
        if (rend.enabled)
        {
            time += Time.deltaTime;
            if(time >= 0.2)
            {
                rend.enabled = false;
                time = 0;
            }
        }
        else
        {
            time += Time.deltaTime;
            if(time >= 0.2)
            {
                rend.enabled = true;
                time = 0;
            }
        }
    }
}
