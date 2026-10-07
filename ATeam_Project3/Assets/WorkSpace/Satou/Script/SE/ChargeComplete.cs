using UnityEngine;

public class ChargeComplete : MonoBehaviour
{
    [SerializeField]
    private AudioSource charegeComplete;

    private bool isPlay = false;

    void Update()
    {
        if (charegeComplete.time >= charegeComplete.clip.length)
        {
            isPlay = true;
        }

    }

    public bool GetIsPlay()
    {
        return isPlay;
    }
}
