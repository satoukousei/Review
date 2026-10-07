using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialInput : MonoBehaviour
{
    [Header("プレイヤーの指定")]
    [SerializeField] private bool isPlayerHero = false;
    [Header("PlayerInput")]
    [SerializeField] private PlayerInput playerInput = null;

    private InputDevice inputDevice;
    private bool isPushSouthButton = false;

    private void Start()
    {
        if (playerInput.devices.Count > 0)
        {
            inputDevice = playerInput.devices[0];
        }
    }

    private void Update()
    {
        if (inputDevice is Gamepad)
        {
            PlayerInputGamepad();
        }
        else
        {
            PlayerInputKeyBoard();
        }
    }

    private void PlayerInputGamepad()
    {
        if (inputDevice is Gamepad gamepad)
        {
            isPushSouthButton = gamepad.buttonSouth.wasPressedThisFrame;
        }
    }

    private void PlayerInputKeyBoard()
    {
        if (isPlayerHero)
        {
            isPushSouthButton = Keyboard.current.spaceKey.wasPressedThisFrame;
        }
        else
        {
            isPushSouthButton = Keyboard.current.enterKey.wasPressedThisFrame;
        }
    }
    public bool GetIsPushSouthBottun()
    {
        return isPushSouthButton;
    }
}