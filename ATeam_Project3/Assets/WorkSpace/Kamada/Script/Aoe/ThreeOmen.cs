using UnityEngine;

public class ThreeOmen : MonoBehaviour
{
    [SerializeField]
    private GameObject[] omen = null;
    [SerializeField]
    private MeshRenderer[] mesh = null;

    private int state = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if(state == 0)
        {
            omen[0].SetActive(true);
            omen[1].SetActive(false);
            omen[2].SetActive(false);

            Fade(0);
        }
        else if(state == 1)
        {
            omen[0].SetActive(false);
            omen[1].SetActive(true);
            omen[2].SetActive(false);
            Fade(1);

        }
        else if(state == 2)
        {
            omen[0].SetActive(false);
            omen[1].SetActive(false);
            omen[2].SetActive(true);
            Fade(2);

        }
    }

    void Fade(int index)
    {
        float alfa = mesh[index].material.color.a;
        alfa -= 2.0f * Time.deltaTime;
        mesh[index].material.color = new Color(mesh[index].material.color.r, mesh[index].material.color.g,
                                               mesh[index].material.color.b, alfa);


        if(mesh[index].material.color.a <= 0.1)
        {
            mesh[index].material.color =  new Color(mesh[index].material.color.r, mesh[index].material.color.g,
                                              mesh[index].material.color.b, 1.0f);
            NextState();
        }
    }

    void NextState()
    {
        if(state == 0)
        {
            state = 1;
        }
        else if (state == 1)
        {
            state = 2;
        }
        else if (state == 2)
        {
            state = 0;
        }
    }
}
