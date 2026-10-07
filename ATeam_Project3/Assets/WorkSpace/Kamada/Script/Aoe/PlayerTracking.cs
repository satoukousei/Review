using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTracking : MonoBehaviour
{
    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private GameObject trackingObject;

    private bool isTrackingPlayer1 = false;
    private bool isTrackingPlayer2 = false;
    private bool isPlay = false;

    private float distanceToPlayer1 = 0;
    private float distanceToPlayer2 = 0;
    private float countTime = 0;
    private float moveSpeed = 0;

    void Update()
    {
        if (isPlay)
        {
            Traking();
        }
    }

    private void FixedUpdate()
    {
        if (isTrackingPlayer1)
        {
            TrackingPlayer1();
        }
        else if (isTrackingPlayer2)
        {
            TrackingPlayer2();
        }
    }

    public void SetTrackData(float speed,float time)
    {
        moveSpeed = speed;
        countTime = time;
    }

    public void StartTraking()
    {
        isPlay = true;
    }

    public void DestroyTrackObject()
    {
        Destroy(this.gameObject);
    }

    private void Traking()
    {
        countTime -= Time.deltaTime;
        if (countTime <= 0)
        {
            //ResetTrack();
            DestroyTrackObject();
        }

        distanceToPlayer1 = Vector3.Distance(trackingObject.transform.position, player1.position);
        distanceToPlayer2 = Vector3.Distance(trackingObject.transform.position, player2.position);

        if (distanceToPlayer1 < distanceToPlayer2)
        {
            isTrackingPlayer1 = true;
            isTrackingPlayer2 = false;
        }
        else
        {
            isTrackingPlayer2 = true;
            isTrackingPlayer1 = false;
        }
    }
    private void TrackingPlayer1()
    {
        Vector3 target = player1.position - trackingObject.transform.position;
        target.y = trackingObject.transform.position.y;
        target.Normalize();
        trackingObject.transform.rotation = Quaternion.LookRotation(target);
        trackingObject.transform.position += moveSpeed * Time.deltaTime * target;
    }
    private void TrackingPlayer2()
    {
        Vector3 target = player2.position - trackingObject.transform.position;
        target.y = trackingObject.transform.position.y;
        target.Normalize();
        trackingObject.transform.rotation = Quaternion.LookRotation(target);
        trackingObject.transform.position += moveSpeed * Time.deltaTime * target;
    }
}
