using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class FlickeringUI : MonoBehaviour
{
    [SerializeField, Header("点滅させるUI")]
    private Image[] images = null;
    [SerializeField]
    private RawImage[] rawImages = null;

    [SerializeField, Header("点滅間隔")]
    private float flickerInterval = 0.5f;
    [SerializeField, Header("点滅時間")]
    private float flickerDuration = 5f;
    [SerializeField, Header("点滅色")]
    private Color flickerColor = Color.red;

    private List<Color> originalColor = new List<Color>();

    private bool isFlickering = false;
    private float endTimer = 0f;
    private float timer = 0f;
    private int count = 0;

    private void Start()
    {
        foreach (var img in images)
        {
            originalColor.Add(img.color);
        }
        foreach (var rawImg in rawImages)
        {
            originalColor.Add(rawImg.color);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(isFlickering)
        {
            timer += Time.deltaTime;
            endTimer += Time.deltaTime;
            if (timer >= flickerInterval)
            {
                count = 0;
                foreach (var img in images)
                {
                    img.color = img.color == originalColor[count] ? flickerColor : originalColor[count];
                    count++;
                }
                foreach (var rawImg in rawImages)
                {
                    rawImg.color = rawImg.color == originalColor[count] ? flickerColor : originalColor[count];
                    count++;
                }
                timer = 0f;
            }
            if(endTimer >= flickerDuration)
            {
                count = 0;
                timer = 0f;
                foreach (var img in images)
                {
                    img.color = originalColor[count];
                    count++;
                }
                foreach (var rawImg in rawImages)
                {
                    rawImg.color = originalColor[count];
                    count++;
                }
                count = 0;
                endTimer = 0f;
                isFlickering = false;

            }
        }
    }
    public void StartFlickering()
    {
        isFlickering = true;    
    }    
}

