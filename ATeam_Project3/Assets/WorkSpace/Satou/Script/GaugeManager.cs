using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;//この行を追加する

public class GaugeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject RedGauge;//赤ゲージのオブジェクト
    [SerializeField]
    private float scaleChangeSpeed = 0f;//ゲージの変化速度
    [SerializeField]
    private float posChangeSpeed = 0f;//ゲージの位置変化速度

       private Vector3 scale = Vector3.zero;
       private Vector3 pos   = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;//FPSを60に固定

        scale = RedGauge.transform.localScale;
        pos   = RedGauge.transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (scale.x >= 1.0f)//ゲージの最大値を1.0fに設定
        {
            scale.x = 1.0f;
            pos.x = 0.0f;
            RedGauge.transform.localScale = scale;
            RedGauge.transform.localPosition = pos;
        }
        if (scale.x <= 0.0f)//Scale0.0の時にscaleとposを固定
        {
            scale.x = 0.0f;
            pos.x = -50.0f;
            RedGauge.transform.localScale = scale;
            RedGauge.transform.localPosition = pos;
        }
    }
    public void PointPlus(int point)
    {
        scale.x += scaleChangeSpeed * point;
        pos.x += posChangeSpeed * point;
        RedGauge.transform.localScale = scale;//ゲージ残量を1フレームごとに1ずつ減らす
        RedGauge.transform.localPosition = pos;
    }
    public void PointMinus()
    {
        scale.x -= scaleChangeSpeed;
        pos.x -= posChangeSpeed;
        RedGauge.transform.localScale = scale;//ゲージ残量を1フレームごとに1ずつ減らす
        RedGauge.transform.localPosition = pos;
    }

   

}
