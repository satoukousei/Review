using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCreate : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs; // プレイヤープレハブ（最大4つ）
    [SerializeField] private PlayerInputManager playerInputManager;
    private bool ready = false;
    public bool Ready { get => ready; }
    private AOEPlayer[] players;
    public AOEPlayer[] Players { get => players; }

    private void Start()
    {
        int gamepadCount = Gamepad.all.Count;//コントローラーの数を確認
        players = new AOEPlayer[4];
        // すでにプレイヤーが参加しているか確認（JoinPlayerが呼ばれているか）
        bool hasGamepadPlayer = gamepadCount > 0;

        if (hasGamepadPlayer)//コントローラーの数が1以上だったら
        {
            int maxPlayers = Mathf.Min(gamepadCount, 4);//プレイヤーの数が4以下にする
            for (int i = 0; i < Mathf.Min(gamepadCount, 4); i++)
            {
                var gamepad = Gamepad.all[i];
                if (gamepad != null)
                {
                    playerInputManager.playerPrefab = playerPrefabs[i];
                    playerInputManager.JoinPlayer(i, -1, null, gamepad);
                }
            }
            if (maxPlayers == 4)
            {
                ready = true;
            }
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        int index = playerInput.user.index;

        // 基準位置（例：原点）から x を1ずつずらして配置
        Vector3 basePosition = Vector3.zero;
        Vector3 offset = new Vector3(1f, 0f, 0f); // x方向に1ずつずらす

        playerInput.transform.position = basePosition + offset * index;
        AOEPlayer aoePlayer = playerInput.GetComponent<AOEPlayer>();
        if (aoePlayer != null)
        {
            players[index] = aoePlayer;
        }
        Debug.Log($"プレイヤー#{index}が入室！ 位置: {playerInput.transform.position}");
    }

}