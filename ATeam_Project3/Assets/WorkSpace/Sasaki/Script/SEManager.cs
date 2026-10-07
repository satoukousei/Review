using UnityEngine;

[System.Serializable]
public class PlayerSE
{
    [SerializeField] private AudioClip catchSuccessSE;      //キャッチ成功
    [SerializeField, Range(0.01f, 10f)] private float catchSccessSEVolume;
    [SerializeField] private AudioClip catchFailSE;         //キャッチ失敗
    [SerializeField, Range(0.01f, 10f)] private float catchFailSEVolume;
    [SerializeField] private AudioClip moveSE;              //移動
    [SerializeField, Range(0.01f, 10f)] private float moveSEVolume;
    [SerializeField] private AudioClip attractSE;           //引き寄せ
    [SerializeField, Range(0.01f, 10f)] private float attractSEVolume;
    [SerializeField] private AudioClip damageSE;            //被弾
    [SerializeField, Range(0.01f, 10f)] private float damageSEVolume;
    [SerializeField] private AudioClip debuffSE;            //デバフ付与
    [SerializeField, Range(0.01f, 10f)] private float debuffSEVolume;

    public AudioClip CatchSuccessSE => catchSuccessSE;
    public float CatchSuccessSEVolume => catchSccessSEVolume;
    public AudioClip CatchFailSE => catchFailSE;
    public float CatchFailSEVolume => catchFailSEVolume;
    public AudioClip MoveSE => moveSE;
    public float MoveSEVolume => moveSEVolume;
    public AudioClip AttractSE => attractSE;
    public float AttractSEVolume => attractSEVolume;
    public AudioClip DamageSE => damageSE;
    public float DamageSEVolume => damageSEVolume;
    public AudioClip DebuffSE => debuffSE;
    public float DebuffSEVolume => debuffSEVolume;

}

[System.Serializable]
public class ItemSE
{
    [SerializeField] private AudioClip heartCatchSE; //ハートキャッチSE
    [SerializeField, Range(0.01f, 10f)] private float heartCatchSEVolume;
    [SerializeField] private AudioClip heartCreateSE;//ハート生成SE
    [SerializeField, Range(0.01f, 10f)] private float heartCreateSEVolume;

    public AudioClip HeartCatchSE => heartCatchSE;
    public float HeartCatchSEVolume => heartCatchSEVolume;
    public AudioClip HeartCreateSE => heartCreateSE;
    public float HeartCreateSEVolume => heartCreateSEVolume;
}

[System.Serializable]
public class BossSE
{
    [SerializeField] private AudioClip voiceSE;         //ボイスSE
    [SerializeField, Range(0.01f, 10f)] private float voiceSEVolume;
    [SerializeField] private AudioClip aoeSE;           //aoeSE
    [SerializeField, Range(0.01f, 10f)] private float aoeSEVolume;
    [SerializeField] private AudioClip handMoveSE;      //手の動き時SE
    [SerializeField, Range(0.01f, 10f)] private float handMoveSEVolume;
    [SerializeField] private AudioClip handAttackSE;    //手の落下時SE
    [SerializeField, Range(0.01f, 10f)] private float handAttackSEVolume;
    [SerializeField] private AudioClip handFallSE;      //手の落下中SE
    [SerializeField, Range(0.01f, 10f)] private float handFallSEVolume;
    [SerializeField] private AudioClip meteorFallSE;    //隕石落下SE
    [SerializeField, Range(0.01f, 10f)] private float meteorFallSEVolume;
    [SerializeField] private AudioClip explosionSE;     //爆発SE
    [SerializeField, Range(0.01f, 10f)] private float explosionSEVolume;
    [SerializeField] private AudioClip beamSelectingSE; //ビームルーレット時SE   
    [SerializeField, Range(0.01f, 10f)] private float beamSelectingSEVolume;
    [SerializeField] private AudioClip beamChargeCompletedSE;
    [SerializeField, Range(0.1f, 10f)]private float beamChargeCompletedSEVolume;//ビームチャージ完了時SE
    [SerializeField] private AudioClip beamAttackSE;    //ビーム攻撃SE 
    [SerializeField, Range(0.01f, 10f)] private float beamAttackSEVolume;
    [SerializeField] private AudioClip fireBallSE;    //ビーム攻撃SE 
    [SerializeField, Range(0.01f, 10f)] private float fireBallSEVolume;
    [SerializeField] private AudioClip magicCircleSE;    //ビーム攻撃SE 
    [SerializeField, Range(0.01f, 10f)] private float magicCircleSEVolume;

    public AudioClip VoiceSE => voiceSE;
    public float VoiceSEVolume => voiceSEVolume;
    public AudioClip AoeSE => aoeSE;
    public float AoeSEVolume => aoeSEVolume;
    public AudioClip HandMoveSE => handMoveSE;
    public float HandMoveSEVolume => handMoveSEVolume;
    public AudioClip HandAttackSE => handAttackSE;
    public float HandAttackSEVolume => handAttackSEVolume;
    public AudioClip HandFallSE => handFallSE;
    public float HandFallSEVolume => handFallSEVolume;
    public AudioClip MeteorFallSE => meteorFallSE;
    public float MeteorFallSEVolume => meteorFallSEVolume;
    public AudioClip ExplosionSE => explosionSE;
    public float ExplosionSEVolume => explosionSEVolume;
    public AudioClip BeamSelectingSE => beamSelectingSE;
    public float BeamSelectingSEVolume => beamSelectingSEVolume;
    public AudioClip BeamAttackSE => beamAttackSE;
    public float BeamAttackSEVolume => beamAttackSEVolume;
    public AudioClip BeamChargeCompletedSE => beamChargeCompletedSE;
    public float BeamCharegeCompletedSEVolume => beamChargeCompletedSEVolume;
    public AudioClip FireBallSE => fireBallSE;
    public float FireBallSEVolume => fireBallSEVolume;
    //public AudioClip MagicCircleSE => magicCircleSE;
    //public float MagicCircleSEVolume => MagicCircleSEVolume;
}

[System.Serializable]
public class EnemySE
{
    [SerializeField] private AudioClip contactSE; //被弾SE
    [SerializeField, Range(0.01f, 10f)] private float contactSEVolume;
    [SerializeField] private AudioClip spawnSE;   //出現SE
    [SerializeField, Range(0.01f, 10f)] private float spawnSEVolume;
    public AudioClip ContactSE => contactSE;
    public float ContactSEVolume => contactSEVolume;
    public AudioClip SpawnSE => spawnSE;
    public float SpawnSEVolume => spawnSEVolume;
}



[System.Serializable]
public class SystemSE
{
    #region SerializeField
    [SerializeField] private AudioClip selectSE;         //選択SE
    [SerializeField, Range(1f, 10f)] private float selectSEVolume;
    [SerializeField] private AudioClip selectDecisionSE; //決定SE
    [SerializeField, Range(1f, 10f)] private float selectDecisionSEVolume;
    [SerializeField] private AudioClip resultSRankSE;    //Result時Sランク時SE
    [SerializeField, Range(1f, 10f)] private float resultSRankSEVolume;
    [SerializeField] private AudioClip resultARankSE;    //Result時Aランク時SE
    [SerializeField, Range(1f, 10f)] private float resultARankSEVolume;
    [SerializeField] private AudioClip resultBRankSE;    //Result時Bランク時SE
    [SerializeField, Range(1f, 10f)] private float resultBRankSEVolume;
    [SerializeField] private AudioClip resultCRankSE;    //Result時Cランク時SE
    [SerializeField, Range(1f, 10f)] private float resultCRankSEVolume;
    [SerializeField] private AudioClip resultDRankSE;    //Result時Dランク時SE
    [SerializeField, Range(0.00001f, 10f)] private float resultDRankSEVolume;
    [SerializeField] private AudioClip helpMakeSE;       //ヘルプ表示SE
    [SerializeField, Range(1f, 10f)] private float helpMakeSEVolume;
    [SerializeField] private AudioClip slowEffectSE;     //スローエフェクトSE
    [SerializeField, Range(1f, 10f)] private float slowEffectSEVolume;
    #endregion

    #region public
    public AudioClip SelectSE => selectSE;
    public float SelectSEVolume => selectSEVolume;
    public AudioClip SelectDecisionSE => selectDecisionSE;
    public float SelectDecisionSEVolume => selectDecisionSEVolume;
    public AudioClip ResultSRankSE => resultSRankSE;
    public float ResultSRankSEVolume => resultSRankSEVolume;
    public AudioClip ResultARankSE => resultARankSE;
    public float ResultARankSEVolume => resultARankSEVolume;
    public AudioClip ResultBRankSE => resultBRankSE;
    public float ResultBRankSEVolume => resultBRankSEVolume;
    public AudioClip ResultCRankSE => resultCRankSE;
    public float ResultCRankSEVolume => resultCRankSEVolume;
    public AudioClip ResultDRankSE => resultDRankSE;
    public float ResultDRankSEVolume => resultDRankSEVolume;
    public AudioClip HelpMakeSE => helpMakeSE;
    public float HelpMakeSEVolume => helpMakeSEVolume;
    public AudioClip SlowEffectSE => slowEffectSE;
    public float SlowEffectSEVolume => slowEffectSEVolume;
    #endregion
}

public class SEManager : MonoBehaviour
{

    [Header("メインシーン")]

    [Header("プレイヤー")]
    [SerializeField]
    private PlayerSE playerSE;

    [Header("敵")]
    [SerializeField]
    private EnemySE enemySE;

    [Header("AOE")]
    [SerializeField]
    private BossSE bossSE;

    [Header("アイテム")]
    [SerializeField]
    private ItemSE itemSE;

    [Header("システム")]
    [SerializeField]
    private SystemSE systemSE;

    [SerializeField] private AudioSource[] laserCharge;


    public static SEManager instance;

    private AudioSource laserCompleted;
    private AudioSource audioSource;
    private int startCount = 0;
    private int stopCount = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    #region アイテムSE再生関数
    public void PlayHeartCatchSE()
    {
        audioSource.clip = itemSE.HeartCatchSE;
        audioSource.volume = itemSE.HeartCatchSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(itemSE.HeartCatchSE);
    }

    public void PlayHeartCreateSE()
    {
        audioSource.clip = itemSE.HeartCreateSE;
        audioSource.volume = itemSE.HeartCreateSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(itemSE.HeartCreateSE);
    }
    #endregion

    #region ボスSE再生関数
    public void PlayBossVoiceSE()
    {
        audioSource.clip = bossSE.VoiceSE;
        audioSource.volume = bossSE.VoiceSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.VoiceSE);
    }
    public void PlayBossAoeSE()
    {
        audioSource.clip = bossSE.AoeSE;
        audioSource.volume = bossSE.AoeSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.AoeSE);
    }
    public void PlayBossHandMoveSE()
    {
        audioSource.clip = bossSE.HandMoveSE;
        audioSource.volume = bossSE.HandMoveSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.HandMoveSE);
    }
    public void PlayBossHandAttackSE()
    {
        audioSource.clip = bossSE.HandAttackSE;
        audioSource.volume = bossSE.HandAttackSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.HandAttackSE);
    }
    public void PlayBossHandFallSE()
    {
        audioSource.clip = bossSE.HandFallSE;
        audioSource.volume = bossSE.HandFallSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.HandFallSE);
    }
    public void PlayBossMeteorFallSE()
    {
        audioSource.clip = bossSE.MeteorFallSE;
        audioSource.volume = bossSE.MeteorFallSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.MeteorFallSE);
    }
    public void PlayBossExplosionSE()
    {
        audioSource.clip = bossSE.ExplosionSE;
        audioSource.volume = bossSE.ExplosionSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.ExplosionSE);
    }
    public void PlayBossBeamSelectingSE()
    {
        if (startCount > laserCharge.Length)
        {
            startCount = 0;
        }
        laserCharge[startCount].clip = bossSE.BeamSelectingSE;
        laserCharge[startCount].volume = bossSE.BeamSelectingSEVolume;
        laserCharge[startCount].pitch = 1.0f;
        laserCharge[startCount].Play();
        startCount++;
    }
    
    public void StopBossChargeSE()
    {
        if(stopCount > laserCharge.Length)
        {
            stopCount = 0;
        }
        laserCharge[stopCount].Stop();
        stopCount++;
    }
    public void PlayBossBeamAttackSE()
    {
        audioSource.clip = bossSE.BeamAttackSE;
        audioSource.volume = bossSE.BeamAttackSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.BeamAttackSE);
    }

    public void PlayBossBeamChargeCompletedSE()
    {
        audioSource.clip = bossSE.BeamChargeCompletedSE;
        audioSource.volume = bossSE.BeamCharegeCompletedSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.BeamChargeCompletedSE);
    }
    public void PlayBossFireBallSE()
    {
        audioSource.clip = bossSE.FireBallSE;
        audioSource.volume = bossSE.FireBallSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(bossSE.FireBallSE);
    }
    //public void PlayBossMagicCircleSE()
    //{
    //    audioSource.clip = bossSE.MagicCircleSE;
    //    audioSource.volume = bossSE.MagicCircleSEVolume;
    //    audioSource.pitch = 1.0f;
    //    audioSource.PlayOneShot(bossSE.MagicCircleSE);
    //}

    public void AudioStop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); 
        }
    }
    #endregion

    #region 敵SE再生関数
    public void PlayEnemyDamageSE()
    {
        audioSource.clip = enemySE.ContactSE;
        audioSource.volume = enemySE.ContactSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(enemySE.ContactSE);
    }
    public void PlayEnemySpawnSE()
    {
        audioSource.clip = enemySE.SpawnSE;
        audioSource.volume = enemySE.SpawnSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(enemySE.SpawnSE);
    }
    #endregion

    #region プレイヤーSE再生関数
    public void PlayPlayerCatchSuccessSE()
    {
        audioSource.clip = playerSE.CatchSuccessSE;
        audioSource.volume = playerSE.CatchSuccessSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(playerSE.CatchSuccessSE);
    }
    public void PlayPlayerCatchFailSE()
    {
        audioSource.clip = playerSE.CatchFailSE;
        audioSource.volume = playerSE.CatchFailSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(playerSE.CatchFailSE);
    }
    public void PlayPlayerMoveSE()
    {
        audioSource.clip = playerSE.MoveSE;
        audioSource.volume = playerSE.MoveSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(playerSE.MoveSE);
    }
    public void PlayPlayerAttractSE()
    {
        audioSource.clip = playerSE.AttractSE;
        audioSource.volume = playerSE.AttractSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(playerSE.AttractSE);
    }
    public void PlayPlayerDamageSE()
    {
        audioSource.clip = playerSE.DamageSE;
        audioSource.volume = playerSE.DamageSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(playerSE.DamageSE);
    }
    public void PlayPlayerDebuffSE()
    {
        audioSource.clip = playerSE.DebuffSE;
        audioSource.volume = playerSE.DebuffSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(playerSE.DebuffSE);
    }
        #endregion

    #region systemSE再生関数
    public void PlaySelectSE()
    {
        audioSource.clip = systemSE.SelectSE;
        audioSource.volume = systemSE.SelectSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(systemSE.SelectSE);
    }

    public void PlaySelectDecisionSE()
    {
        audioSource.clip = systemSE.SelectDecisionSE;
        audioSource.volume = systemSE.SelectDecisionSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(systemSE.SelectDecisionSE);
    }
    public void PlayResultRankSE(string rank)
    {
        switch (rank)
        {
            case "S":
                audioSource.clip = systemSE.ResultSRankSE;
                audioSource.volume = systemSE.ResultSRankSEVolume;
                break;
            case "A":
                audioSource.clip = systemSE.ResultARankSE;
                audioSource.volume = systemSE.ResultARankSEVolume;
                break;
            case "B":
                audioSource.clip = systemSE.ResultBRankSE;
                audioSource.volume = systemSE.ResultBRankSEVolume;
                break;
            case "C":
                audioSource.clip = systemSE.ResultCRankSE;
                audioSource.volume = systemSE.ResultCRankSEVolume;
                break;
            case "D":
                audioSource.clip = systemSE.ResultDRankSE;
                audioSource.volume = systemSE.ResultDRankSEVolume;
                break;
            default:
                Debug.LogError("Invalid rank: " + rank);
                return;
        }
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void PlayHelpMakeSE()
    {
        audioSource.clip = systemSE.HelpMakeSE;
        audioSource.volume = systemSE.HelpMakeSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(systemSE.HelpMakeSE);
    }
    public void PlaySlowEffectSE()
    {
        audioSource.clip = systemSE.SlowEffectSE;
        audioSource.volume = systemSE.SlowEffectSEVolume;
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(systemSE.SlowEffectSE);
    }
    #endregion
}
