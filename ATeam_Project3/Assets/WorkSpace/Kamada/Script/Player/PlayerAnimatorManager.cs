using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerMove move;
    [SerializeField] private PlayerPull pull;
    [SerializeField] private PlayerPull targetPull;
    [SerializeField] private PlayerPullData pullData;
    [SerializeField] private Knockback knockback;
    [SerializeField] private GameObject stunEffect;

    private float countTime = 0f;
    private float stunDelayTime = 0f;

    private bool isDelay = false;
    private bool isPlayCatchAnim = false;
    private bool isPullingMissCatch= false;
    private bool isBeingPullingMissCatch = false;

    private PlayerState currentState = PlayerState.Idle;

    private void Start()
    {
        stunEffect.SetActive(false);
    }
    private enum PlayerState
    {
        Idle = 0,
        Move = 1,
        Pull = 2,
        Catch = 3,
        BeingPulled = 4,
        KnockBack = 5,
        Pull_MissCatch = 6,
        BeingPulled_MissCatch = 7,
        a = 10,
    }
    private void Update()
    {
        UpdateDelay();  // キャッチ用ディレイ

        if (isDelay) return;// キャッチ用のディレイ中は状態を変更しない

        DecideState();// 状態を決定してアニメーションを切り替える

        if (isPullingMissCatch)
        {
            stunDelayTime += Time.deltaTime;
            if (stunDelayTime >= 1.0f && !stunEffect.activeSelf)
            {
                stunEffect.SetActive(true);
            }

            if (anim.GetInteger("Player") != (int)PlayerState.Pull_MissCatch)
            {
                isPullingMissCatch = false;
            }
        }
        else if (isBeingPullingMissCatch)
        {
            stunDelayTime += Time.deltaTime;
            if (stunDelayTime >= 1.0f && !stunEffect.activeSelf)
            {
                stunEffect.SetActive(true);
            }

            if (anim.GetInteger("Player") != (int)PlayerState.BeingPulled_MissCatch)
            {
                isBeingPullingMissCatch = false;
            }
        }
        else
        {
            if (stunEffect.activeSelf)
            {
                stunEffect.SetActive(false);
            }

            if (stunDelayTime != 0)
            {
                stunDelayTime = 0;
            }
        }
    }

    private void DecideState()// プレイヤーの状態を決定する
    {
        PlayerState newState = PlayerState.Idle;

        if (knockback.GetIsKnockBack())
        {
            newState = PlayerState.KnockBack;
            knockback.SetKnockBack();
        }
        else if (pull.GetIsCatch() && !isPlayCatchAnim || targetPull.GetIsCatch() && !isPlayCatchAnim)
        {
            newState = PlayerState.Catch;
            StartCatchDelay();
        }
        else if (pull.GetIsMissCatch())
        {
            newState = PlayerState.Pull_MissCatch;
            isPullingMissCatch = true;
        }
        else if (pull.GetIsBegingPulledMissCatch())
        {
            newState = PlayerState.BeingPulled_MissCatch;
            isBeingPullingMissCatch = true;
        }
        else if (pull.GetIsBeingPulled())
        {
            newState = PlayerState.BeingPulled;
        }
        else if (pull.GetIsPulling())
        {
            newState = PlayerState.Pull;
        }
        else if (move.GetIsMove())
        {
            newState = PlayerState.Move;
        }

        isPlayCatchAnim = pull.GetIsCatch() || targetPull.GetIsCatch();
        ChangeState(newState);
    }

    private void ChangeState(PlayerState newState)// 状態を変更してアニメーションを切り替える
    {
        if (currentState == newState) return;

        currentState = newState;
        anim.SetInteger("Player", (int)currentState);
    }
    private void StartCatchDelay()// キャッチ用のディレイを開始する
    {
        isDelay = true;
        countTime = 0f;
    }

    private void UpdateDelay()// キャッチ用のディレイを更新する
    {
        if (!isDelay) return;

        float index = 1.0f / Time.timeScale;
        countTime += Time.deltaTime * index;

        if (countTime >= pullData.GetCatchDelayTime())
        {
            countTime = 0f;
            isDelay = false;
        }
    }
}