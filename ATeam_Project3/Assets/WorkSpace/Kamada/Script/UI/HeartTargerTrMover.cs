using UnityEngine;

public class HeartTargerTrMover : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTr;
    [SerializeField]
    private Transform targetTr;
    [SerializeField]
    private HelpUIAnimation heartStop;
    private void Update()
    {
        if (!heartStop.IsAnimating())
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetTr.position);
            rectTr.position = screenPos;
        }
    }
}
