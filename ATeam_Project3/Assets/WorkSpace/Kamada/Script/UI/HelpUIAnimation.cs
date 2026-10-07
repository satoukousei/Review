using UnityEngine;

public class HelpUIAnimation : MonoBehaviour
{
    [SerializeField]
    private RectTransform heartImage = null;
    [SerializeField]
    private Vector2 imageScale = new Vector2(1.0f, 1.0f);
    [SerializeField]
    private float imageChangeTime = 0.1f;
    [SerializeField]
    private GameObject imageObject;
    
    private Vector2 maxImageCount = new Vector2(0.0f, 0.0f);
    private Vector2 currentImageCount = new Vector2(0, 0);
    private float timer = 0.0f;
    private bool isAnimating = false;
    private bool isEnd = false;
    private void Awake()
    {
        maxImageCount = new Vector2(heartImage.rect.width / imageScale.x, heartImage.rect.height / imageScale.y);
    }



    private void Update()
    {
        if (isAnimating)
        {
            timer += Time.deltaTime;
            if (timer >= imageChangeTime)
            {
                ImageMove();
                timer = 0.0f;
            }
        }
    }

    private void ImageMove()
    {
        heartImage.anchoredPosition = new Vector2((imageScale.x * currentImageCount.x) * -1, imageScale.y * currentImageCount.y);
        currentImageCount.x++;
        if (currentImageCount.x >= maxImageCount.x)
        {
            currentImageCount.x = 0;
            currentImageCount.y++;
            if (currentImageCount.y >= maxImageCount.y)
            {
                currentImageCount.y = 0;
                isEnd = true;
            }
        }
    }

    public void StartAnimation()
    {
        if (isAnimating)
        {
            return;
        }
        isAnimating = true;
        
    }
    public void StopAnimation()
    {
        isAnimating = false;
        currentImageCount = new Vector2(0, 0);
        timer = 0.0f;
        isEnd = false;
    }

    public bool IsEnd()
    {
        return isEnd;
    }
    public bool IsAnimating()
    {
        return isAnimating;
    }

    public void IsInstantiate()
    {
        imageObject.SetActive(true);
    }
    public void IsDelete()
    {
        imageObject.SetActive(false);
    }
}
