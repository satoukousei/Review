using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField,Header("移動速度")] 
    private float speed = 3;
    [SerializeField, Header("スロウ時の移動速度")]
    private float slowSpeed = 2;
    [SerializeField]
    private float slowTime = 0;
    [SerializeField]
    private ParticleSystem effect = null;
    [SerializeField]
    private bool player1 = false;
    [SerializeField]
    private FadeCreate fade = null;
    [SerializeField]
    private TestStun testStun = null;

    private PlayerInput playerInput;
    private InputDevice inputDevice;

    private Rigidbody rb;
    private Vector3 moveVelocity = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private bool isPushSouthButton = false;
    private bool isMove = false;
    private bool isSlow = false;
    private bool isStun = false;

    private float moveSpeed = 0;
    private float countTime = 0;

    private void OnMove(InputValue value)
    {
        var axis = value.Get<Vector2>();
        moveVelocity = new Vector3(axis.x, 0, axis.y);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        if (playerInput.devices.Count > 0)
        {
            inputDevice = playerInput.devices[0];
        }
        moveSpeed = speed;
    }

    private void Update()
    {
        if (!fade.IsStartFadeEnd)
        {
            return;
        }
        
        if (inputDevice is Gamepad)//ボタン入力はスタンに干渉しない
        {
            PlayerInputGamepad();
        }
        else
        {
            PlayerInputKeyBoard();
        }

        if (testStun.IsStun())//スタン時に動きだけ制限
        {
            return;
        }

        Move();
        Slow();
    }
    private void Slow()
    {
        if (isSlow)
        {
            if (effect.isStopped)
            {
                effect.Play();
            }
            countTime -= Time.deltaTime;
            if (countTime <= 0)
            {
                isSlow = false;
                SetMoveSpeed();
                effect.Stop();
            }
        }
    }
    private void Move()
    {
        if (!isStun)
        {
            if (inputDevice is Gamepad)
            {
                Vector3 move = moveVelocity * moveSpeed * Time.deltaTime;
                Vector3 newPos = rb.position + new Vector3(move.x, 0, move.z);
                rb.MovePosition(newPos);

                // プレイヤーの向きを進行方向に合わせる
                if (moveVelocity != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveVelocity.x, 0, moveVelocity.z));
                    rb.rotation = targetRotation; // 瞬時に回転させる
                    isMove = true;
                }
                else
                {
                    isMove = false;
                }
            }
            else
            {
                Vector3 move = velocity * moveSpeed * Time.deltaTime;
                Vector3 newPos = rb.position + new Vector3(move.x, 0, move.z);
                rb.MovePosition(newPos);

                // プレイヤーの向きを進行方向に合わせる
                if (velocity != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z));
                    rb.rotation = targetRotation; // 瞬時に回転させる
                    isMove = true;
                }
                else
                {
                    isMove = false;
                }
                PlayerMoveInputKeyboard();
            }
        }
        else
        {
            isMove = false;
        }
    }

    private void PlayerInputGamepad()
    {
        if (inputDevice is Gamepad gamepad)
        {
            if (gamepad != null)
            {
                if (gamepad.buttonSouth.wasPressedThisFrame)
                {
                    isPushSouthButton = true;
                }
                else
                {
                    isPushSouthButton = false;
                }
            }
        }
    }
    private void PlayerMoveInputKeyboard()
    {
        velocity = Vector3.zero;

        if (player1)
        {
            if(Keyboard.current.wKey.isPressed)
            {
                velocity += new Vector3(0, 0, 1);
            }
            if (Keyboard.current.sKey.isPressed)
            {
                velocity += new Vector3(0, 0, -1);
            }
            if (Keyboard.current.aKey.isPressed)
            {
                velocity += new Vector3(-1, 0, 0);
            }
            if (Keyboard.current.dKey.isPressed)
            {
                velocity += new Vector3(1, 0 , 0);
            }
        }
        else
        {
            if (Keyboard.current.upArrowKey.isPressed)
            {
                velocity += new Vector3(0, 0, 1);
            }
            if (Keyboard.current.downArrowKey.isPressed)
            {
                velocity += new Vector3(0, 0, -1);
            }
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                velocity += new Vector3(-1, 0, 0);
            }
            if (Keyboard.current.rightArrowKey.isPressed)
            {
                velocity += new Vector3(1, 0, 0);
            }
        }

        velocity.Normalize();
    }

    private void PlayerInputKeyBoard()
    {
        if (player1)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                isPushSouthButton = true;
            }
            else
            {
                isPushSouthButton = false;
            }
        }
        else
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                isPushSouthButton = true;
            }
            else
            {
                isPushSouthButton = false;
            }
        }
    }

    private void SetMoveSpeed()
    {
        if(isSlow)
        {
            moveSpeed = slowSpeed;
        }
        else
        {
            moveSpeed = speed;
        }
    }

    public void SetIsSlow(bool isSlow_)
    {
        isSlow = isSlow_;
        countTime = slowTime;
        SetMoveSpeed();
    }

    public bool GetIsMove() {return isMove;}
    public bool GetIsPushSouthBottun()
    {
        return isPushSouthButton;
    }
    public bool SetStunFlag(bool isStan_)
    {
        isStun = isStan_;
        return isStun;
    }
}