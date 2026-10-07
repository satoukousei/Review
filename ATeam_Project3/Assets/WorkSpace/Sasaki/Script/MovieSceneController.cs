using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class MovieSceneController : MonoBehaviour
{
    [SerializeField]
    private FadeCreate FadeCreater;
    [SerializeField]
    private VideoPlayer video;
    private void Start()
    {
        // 再生終了時に呼ばれるイベントにメソッドを登録
        video.loopPointReached += OnVideoEnd;
    }

    // ビデオが最後まで再生された時に実行される
    private void OnVideoEnd(VideoPlayer vp)
    {
        FadeCreater.FadeOutStart();
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            FadeCreater.FadeOutStart();
        }
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                FadeCreater.FadeOutStart();
            }
        }
    }
}
