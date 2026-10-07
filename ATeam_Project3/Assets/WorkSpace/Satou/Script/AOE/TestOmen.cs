using UnityEngine;

public class TestOmen : MonoBehaviour
{
    //AOEを徐々に広げるエフェクト
    [SerializeField]
    private GameObject omen;
    [SerializeField]
    private GameObject maxOmen;
    [SerializeField]
    private float expandSpeed = 0.0f;


    void Start()
    {
        omen.transform.localScale = new Vector3(0, 0.1f, 0);
    }

    // Update is called once per frame
    void Update()
    {

            if (omen.transform.localScale.x <= maxOmen.transform.localScale.x)
            {
                omen.transform.localScale += new Vector3(expandSpeed, 0, expandSpeed) * Time.deltaTime;
            }
      

    }
}
