using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CountTimeManager : MonoBehaviour
{
    [SerializeField]
    private float MaxTime;
    [SerializeField]
    private TextMeshProUGUI timeText;
    private float timer;


    void Start()
    {
        timer = MaxTime;
        timeText.text = timer.ToString("F2");
    }

    void Update()
    {
        CountTime();
    }
    private void CountTime()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
        }
        timeText.text = timer.ToString("F2");
    }



}
