using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.position += new Vector3(0.0f, 0.0f, 4.0f) * Time.deltaTime;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.position += new Vector3(0.0f, 0.0f, -4.0f) * Time.deltaTime;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position += new Vector3(4.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position += new Vector3(-4.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
    }
}
