using UnityEngine;

public class Expand_Shrink : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float maxSize = 2f;
    [SerializeField] private float minSize = 0.5f;
    [SerializeField] private float duration = 1f; // 何秒で移動するか

    private bool isExpand = true;
    private bool isPlay = false;

    private void Start()
    {
        target.transform.localScale = new Vector3(minSize, minSize, minSize);
    }

    private void Update()
    {
        if (!isPlay)
        {
            return;
        }

        Vector3 scale = target.transform.localScale;

        float scaleSpeed = (maxSize - minSize) / duration;

        if (isExpand)
        {
            scale += Vector3.one * scaleSpeed * Time.deltaTime;

            if (scale.x >= maxSize)
            {
                scale = Vector3.one * maxSize;
                isExpand = false;
            }
        }
        else
        {
            scale -= Vector3.one * scaleSpeed * Time.deltaTime;

            if (scale.x <= minSize)
            {
                scale = Vector3.one * minSize;
                isExpand = true;
            }
        }

        target.transform.localScale = scale;
    }

    public void PlayExpandShrink()
    {
        if (isPlay)
        {
            return;
        }

        target.transform.localScale = Vector3.one * minSize;
        isPlay = true;
    }

    public void StopExpandShrink()
    {
        isPlay = false;
    }
}