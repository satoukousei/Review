using Unity.VisualScripting;
using UnityEngine;

public class PlayerPullData : MonoBehaviour
{
    [Header("引っ張っているときの移動速度")]
    [SerializeField]
    private float moveSpeed = 0;
    [Header("引張を止める距離")]
    [SerializeField]
    private float stopDistance = 0;
    [Header("引張を始められる距離")]
    [SerializeField]
    private float canPullDistance = 0;
    [Header("キャッチの許容時間")]
    [SerializeField]
    private float canPullTime = 0;
    [Header("ノックバックの力")]
    [SerializeField]
    private float knockBackPower = 0;
    [Header("引っ張り時に減速を始める距離")]
    [SerializeField]
    private float slowDistance = 0;
    [Header("減速時の速度")]
    [SerializeField]
    private float slowTime = 0;
    [Header("キャッチ成功最小距離")]
    [SerializeField]
    private float minCatchDistance = 0;
    [Header("キャッチ成功最大距離")]
    [SerializeField]
    private float maxCatchDistance = 0;
    [Header("キャッチ可能距離")]
    [SerializeField]
    private float catchDistance = 0;
    [Header("キャッチ時の減速率")]
    [SerializeField]
    private float catchDeceleration = 0;
    [Header("キャッチ時のディレイ時間")]
    [SerializeField]
    private float catchDelayTime = 0;
    [Header("キャッチ失敗時の引張を止める距離")]
    [SerializeField]
    private float missStopDistance = 0;
    [Header("キャッチ失敗時時のスタン時間")]
    [SerializeField]
    private float stunTime = 0;

    public float GetMoveSpeed() { return moveSpeed; }
    public float GetStopDistance() { return stopDistance; }
    public float GetCanPullDistance() { return canPullDistance; }
    public float GetCanPullTime() { return canPullTime; }
    public float GetKnockBackPower() { return knockBackPower; }
    public float GetSlowDistance() { return slowDistance; }
    public float GetSlowTime() { return slowTime; }
    public float GetMinCatchDistance() { return minCatchDistance; }
    public float GetMaxCatchDistance() { return maxCatchDistance; }
    public float GetCatchDistance() { return catchDistance; }
    public float GetCatchDelection() { return catchDeceleration; }
    public float GetCatchDelayTime() { return catchDelayTime; }
    public float GetMissStopDistance() { return missStopDistance; }
    public float GetStunTime() { return stunTime; }
}