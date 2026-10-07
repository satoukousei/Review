using UnityEngine;
using UnityEngine.InputSystem;

public class Cutin : MonoBehaviour
{
    [SerializeField]
    private GameObject Image;
    [SerializeField]
    private float maxTimer = 0.0f;

    private float timer = 0.0f;
    private bool isActive = false;

    void Start()
    {
        Image.SetActive(false);
    }

    void Update()
    {
        if(Gamepad.current.buttonSouth.isPressed)
        {
            isActive = true;
        }
        if (isActive)
        {
            Image.SetActive(true);
            CutIn();
        }
    }

    private void CutIn()
    {            
        timer += Time.deltaTime;
        // 位置を直接変更するには、一度変数に代入してから再設定する必要があります
        Vector3 pos = Image.transform.position;
        pos.x -= 10.0f; // ここで加算する値は適宜調整してください
 
        if(timer == maxTimer)
        {
            Image.SetActive(false);
            timer = 0.0f;
        }
    }
}
