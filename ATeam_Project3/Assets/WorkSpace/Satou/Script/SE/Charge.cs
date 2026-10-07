using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField]
    private AudioSource charge;
    [SerializeField]
    private AudioClip chargeclip;

    private bool isPlay = false;
    private bool test = false;


    private float time = 0f;

    private void Start()
    {
        charge.clip = chargeclip;
        charge.Play();
        test = true;

    }
    void Update()
    {
        if (charge.time >= charge.clip.length)
        {
            isPlay = true;
        }

        if (test)
        {
            time += Time.deltaTime;
            if (time > 2.0f)
            {
                charge.GetComponent<Charge>().PlayStop();
                time = 0f;
            }
        }
    }

    public void PlayStop()
    {
        charge.Stop();
    }
    public bool GetIsPlay()
    {
        return isPlay;
    }
}
