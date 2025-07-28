using System;
using UnityEngine;

public enum CollectType
{
    Potion,
    Relic
}

public enum StatType
{
    None,
    Atk,
    Def,
    Stamina,
    Hp,
    Recovery,
    Ultimit,
}

[Serializable]
public struct StatDelta
{
    public StatType statType;
    public int amount;
}

/// <summary>
/// 수집 오브젝트 데이터
/// </summary>
[CreateAssetMenu(fileName = "CollectObjectSO", menuName = "New CollectObjectSO")]
public class CollectObjectSO : ScriptableObject
{
    [Header("분류")]
    public string UID;
    public CollectType collectType;

    [Header("Potion")]
    public bool isMaxIncrease;
    public Sprite potionIcon;

    [Header("Relic")]
    public int index;
    public string relicName;
    public string effectDesc;
    public string relicDesc;
    public Sprite relicSprite;
    public StatDelta[] statDeltas;
}
