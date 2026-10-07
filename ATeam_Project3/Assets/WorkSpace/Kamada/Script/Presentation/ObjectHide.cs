using UnityEngine;

public class ObjectHide : MonoBehaviour
{
    [SerializeField]
    private float hideTime = 0;
    [SerializeField]
    private float coolTime = 0;

    private Renderer rend;
    private BoxCollider testCollider;

    private bool isHide = false;
    bool isOmenPlay = false; 
    private float countCoolTime = 0;
    private float countHideTime = 0;
   
    private void Start()
    {
        rend = GetComponent<Renderer>();
        testCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (isHide)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Hide()
    {
        countCoolTime += Time.deltaTime;

        if (countCoolTime > coolTime)
        {
            isHide = true;
            countCoolTime = 0;
            rend.enabled = false;
            testCollider.enabled = false;
            isOmenPlay = false;

        }

        if (countCoolTime > coolTime - 2)
        {
            isOmenPlay = true;
        }
    }
    private void Show()
    {
        countHideTime += Time.deltaTime;

        if (countHideTime > hideTime)
        {
            isHide = false;
            countHideTime = 0;
            rend.enabled = true;
            testCollider.enabled = true;
        }
    }

    public bool GetHideStatus()
    {
        return isHide;
    }

    public bool GetShowStatus()
    {
        return !isHide;
    }

    public bool IsOmenPlay()
    {
        return isOmenPlay; 
    }
}
