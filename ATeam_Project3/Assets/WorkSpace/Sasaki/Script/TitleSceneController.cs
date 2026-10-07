using UnityEngine;
using UnityEngine.InputSystem;

public class TitleSceneController : MonoBehaviour
{
    [SerializeField]
    private FadeCreate FadeCreater = null;
    [SerializeField]
    private FlickeringUI flickeringUI = null;
    [SerializeField]
    private Fade image = null;
    [SerializeField]
    private SEManager se = null;

    private bool isFaded = false;
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!isFaded)
            {
                OnFade();
            }
        }
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.aButton.wasPressedThisFrame)
            {
                if (!isFaded)
                {
                    OnFade();
                }
            }
        }
    }
    private void OnFade()
    {
        flickeringUI.StartFlickering();
        //FadeCreater.FadeOutStart();
        image.FadeOut("IntroductionScene");
        se.PlaySelectSE();
        isFaded = true;
    }
}
