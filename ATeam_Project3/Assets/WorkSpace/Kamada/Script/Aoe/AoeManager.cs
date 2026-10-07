using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class AoeManager : MonoBehaviour
{
    [Header("攻撃用オブジェクトの設定")]
    [SerializeField] private GameObject[] aoeObject;
    [Header("勇者の引っ張り情報")]
    [SerializeField] private PlayerPull heroPull = null;
    [Header("魔王の娘の引っ張り情報")]
    [SerializeField] private PlayerPull womenPull = null;
    [Header("フェーズ")]
    [SerializeField] private PhaseManager phase = null;
    [Header("フェード")]
    [SerializeField] private FadeCreate fade = null;

    private int count = 0;
    private float countCool = 0;

    /// <summary>
    /// AOEの種類をわかりやすくするために変数に
    /// </summary>
    public enum AoeType
    {
        Fall = 0,
        Laser = 1,
        Roulette = 2,
        Bind = 3,
        Clap = 4,
        Barrage = 5,
        MultiBarrage = 6,
        NextPhase = 7,
    }
    public List<AoeData> aoeData = new List<AoeData>();
    /// <summary>
    /// CSVから取得した情報を保存する
    /// </summary>
    public struct AoeData
    {
        public float coolTime;
        public AoeType type;
        public float omenTime;
        public float fadeTime;
        public float displayTime;
        public float speed;
        public float angle;
        public float size;
        public int rouletteDir;
        public float moveSpeed;
        public int targetPlayer;
        public int spawnPos;
        public int laserMove;
        public int trackObjNumber;
        public string bulletWayPoint;
        public float barrageMinAngle;
        public float barrageMaxAngle;
    }

    private List<IAoe> activeAoes = new List<IAoe>();//現在アクティブなAoeのリスト
    private void Start()
    {
        aoeData.Clear();
        string path = Path.Combine(Application.streamingAssetsPath, "Aoe.csv");
        var lines = File.ReadAllLines(path, Encoding.UTF8);
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("END") || lines[i].StartsWith("end"))
            {
                break;
            }

            var values = lines[i].Split(',');

            aoeData.Add(new AoeData
            {
                coolTime = ParseFloat(values[0]),        //クールタイム
                type = (AoeType)ParseInt(values[1]),     //Aoeの種類
                omenTime = ParseFloat(values[2]),        //予兆時間
                fadeTime = ParseFloat(values[3]),        //予兆アニメーション時間
                displayTime = ParseFloat(values[4]),     //攻撃表示時間
                speed = ParseFloat(values[5]),           //主に動き、回転などの速度
                angle = ParseFloat(values[6]),           //攻撃時の最終角度
                size = ParseFloat(values[7]),            //サイズ
                rouletteDir = ParseInt(values[8]),       //ルーレットの回転方向
                moveSpeed = ParseFloat(values[9]),       //主に、レーザーの動きなどの速度
                targetPlayer = ParseInt(values[10]),     //ターゲットの指定
                spawnPos = ParseInt(values[11]),         //ルーレットが止まる位置
                laserMove = ParseInt(values[12]),        //レーザーが動くかどうか
                bulletWayPoint = values[13],             //多重弾幕の弾道パターン
                barrageMinAngle = ParseFloat(values[14]),//弾幕の最小角度
                barrageMaxAngle = ParseFloat(values[15]) //弾幕の最大角度
            });
        }
    }
    private float ParseFloat(string s)//文字列をfloatに変換
    {
        float.TryParse(s, out float v);
        return v;
    }

    private int ParseInt(string s)//文字列をintに変換
    {
        int.TryParse(s, out int v);
        return v;
    }

    private void Update()
    {
        //終了判定を確認
        for (int i = activeAoes.Count - 1; i >= 0; i--)
        {
            if (activeAoes[i].IsFinished())
            {
                activeAoes[i].OnFinish();
                activeAoes.RemoveAt(i);
            }
        }

        //フェードが終わるまでカウントを止める
        if (!fade.IsStartFadeEnd)
        {
            return;
        }

        //プレイヤーのどちらかが引っ張っていたら
        if (heroPull.GetIsStopAoe() || womenPull.GetIsStopAoe())
        {
            return;
        }

        //カウントがAOEの最大数を超えたら
        if (count >= aoeData.Count)
        {
            return;
        }

        //カウントを数えてカウントがクールタイムを超えたら
        if (countCool >= aoeData[count].coolTime)
        {
            SpawnAoe();
        }
        else
        {
            countCool += Time.unscaledDeltaTime;
        }
    }

    private void SpawnAoe()//Aoe生成処理
    {
        var data = aoeData[count];
        IAoe aoe = null;

        switch (data.type)
        {
            case AoeType.Fall://落下Aoe
                {
                    var obj = Instantiate(aoeObject[0]);
                    var fall = obj.GetComponent<ObjectFallAoe>();

                    fall.SetAoe(
                        data.omenTime,
                        data.fadeTime,
                        data.displayTime,
                        data.size,
                        data.spawnPos
                    );
                    fall.AoeStart();
                    aoe = fall;
                    break;
                }
            case AoeType.Laser://レーザーAoe
                {
                    var obj = Instantiate(aoeObject[1]);
                    var laser = obj.GetComponent<LaserAoe>();

                    laser.SetLaserAoe(
                        data.spawnPos,
                        data.angle,
                        data.size,
                        data.omenTime,
                        data.displayTime
                        );

                    laser.LaserPlay();
                    aoe = laser;
                    break;
                }
            case AoeType.Roulette://ルーレットAoe
                {
                    var obj = Instantiate(aoeObject[2]);
                    var roulette = obj.GetComponent<RouletteAoe>();

                    roulette.SetRoulette(
                        data.omenTime,
                        data.fadeTime,
                        data.displayTime,
                        data.speed,
                        data.spawnPos
                        );
                    roulette.RouletteStart();
                    aoe= roulette;
                    break;
                }
            case AoeType.Bind://拘束Aoe
                {
                    var obj = Instantiate(aoeObject[3]);
                    var bind = obj.GetComponent<BindAoe>();

                    bind.SetAoe(
                        data.displayTime,
                        data.omenTime,
                        data.speed,
                        data.moveSpeed,
                        data.targetPlayer
                        );
                    bind.AoeStart();
                    aoe = bind;
                    break;
                }
            case AoeType.Clap://拍手Aoe
                {
                    var obj = Instantiate(aoeObject[4]);
                    var clap = obj.GetComponent<Clap>();

                    clap.SetAoe(
                        data.omenTime,
                        data.fadeTime,
                        data.displayTime,
                        data.targetPlayer,
                        data.speed,
                        data.angle
                        );
                    clap.AoeStart();
                    aoe = clap;
                    break;
                }
            case AoeType.Barrage://弾幕Aoe
                {
                    var obj = Instantiate(aoeObject[5]);
                    var barrage = obj.GetComponent<BarrageAoe>();
                    barrage.SetBarrageAoe(
                        data.spawnPos,
                        data.rouletteDir,
                        data.speed,
                        data.moveSpeed,
                        data.displayTime,
                        data.barrageMinAngle,
                        data.barrageMaxAngle,
                        data.size
                        );
                    StartCoroutine(barrage.PlayBarrageAoe());
                    aoe = barrage;
                    break;
                }
            case AoeType.MultiBarrage://多重弾幕Aoe
                {
                    var obj = Instantiate(aoeObject[6]);
                    var multiBarrage = obj.GetComponent<MultiBarrageAoe>();
                    multiBarrage.SetMultiBarrageAoe(
                        data.spawnPos,
                        data.bulletWayPoint,
                        data.moveSpeed,
                        data.size
                        );
                    multiBarrage.PlayMultiBarrageAoe();
                    aoe = multiBarrage;
                    break;
                }
            case AoeType.NextPhase://フェーズ切り替え
                {
                    phase.SetPhase((int)data.omenTime,data.fadeTime);
                    phase.PlayPhase();
                    break;
                }
            default:
                break;
        }

        if (aoe != null)//生成したAoeをアクティブリストに追加
        {
            activeAoes.Add(aoe);
        }

        countCool = 0;//クールタイムリセット
        count++;      //次のAoeデータへ
    }
}