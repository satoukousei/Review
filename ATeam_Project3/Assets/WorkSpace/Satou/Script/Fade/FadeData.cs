using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AoeCsvData", menuName = "ScriptableObject/AOE CSV Data")]
public class FadeData : ScriptableObject
{
    public float aoeDelayTime;    // CSVで読み出したフェード時間

    public static float AoeDelayTime { get; internal set; }

    internal void LoadFromAoeCsv(AoeCsv aoeCsv)
    {
        throw new NotImplementedException();
    }
}