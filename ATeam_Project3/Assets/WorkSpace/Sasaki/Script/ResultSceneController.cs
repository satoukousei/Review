using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class ResultSceneController : MonoBehaviour
{
    [SerializeField]
    private FadeCreate FadeCreater;
    [SerializeField]
    private AudioSource se;
    [SerializeField]
    private ScoreMovie scoreMovie;
    [SerializeField]
    private float sceneChangeTime = 0;

    private bool isSceneChanged = false;
    private float countTimer = 0;

    private void Update()
    {
        if (scoreMovie.GetIsPlay())
        {
            if (!isSceneChanged)
            {
                countTimer += Time.deltaTime;

                if(countTimer >= sceneChangeTime)
                {
                    countTimer = 0;
                    FadeCreater.NextSceneSet("TitleScene");
                    FadeCreater.FadeOutStart();
                    TestSEPlay();
                    isSceneChanged = true;
                }

                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    FadeCreater.NextSceneSet("TitleScene");
                    FadeCreater.FadeOutStart();
                    TestSEPlay();
                    isSceneChanged = true;
                }
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    FadeCreater.NextSceneSet("MainScene");
                    FadeCreater.FadeOutStart();
                    TestSEPlay();
                    isSceneChanged = true;
                }

                if (Gamepad.current != null)
                {
                    if (Gamepad.current.aButton.wasPressedThisFrame)
                    {
                        FadeCreater.NextSceneSet("TitleScene");
                        FadeCreater.FadeOutStart();
                        TestSEPlay();
                        isSceneChanged = true;
                    }
                    if (Gamepad.current.bButton.wasPressedThisFrame)
                    {
                        FadeCreater.NextSceneSet("MainScene");
                        FadeCreater.FadeOutStart();
                        TestSEPlay();
                        isSceneChanged = true;
                    }
                }
            }
        }
    }

    private void TestSEPlay()
    {
        se.Play();
    }

}
