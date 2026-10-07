using UnityEngine;
using TMPro; // TextMeshPro‚ðŽg‚¤ê‡

public class FPSCounter : MonoBehaviour
{
    public TMP_Text fpsText; // Inspector‚ÅÝ’è
    private float timer;
    private int frameCount;

    void Update()
    {
        timer += Time.deltaTime;
        frameCount++;

        // 1•bŒo‰ß‚·‚é‚²‚Æ‚ÉFPS‚ðŒvŽZ‚µ‚Ä•\Ž¦
        if (timer >= 1.0f)
        {
            int fps = (int)(frameCount / timer);
             //Debug.Log("FPS: " + fps.ToString());

            timer = 0.0f;
            frameCount = 0;
        }
    }
}