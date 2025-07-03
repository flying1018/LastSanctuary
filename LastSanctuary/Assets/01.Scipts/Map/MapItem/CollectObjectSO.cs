using UnityEngine;

public enum CollectType
{
    Potion,
    Relic
}

[CreateAssetMenu(fileName = "CollectObjectSO", menuName = "New CollectObjectSO")]
public class CollectObjectSO : ScriptableObject
{
    [Header("분류")]
    public string UID;
    public bool isConsumable;
    public CollectType collectType;

    [Header("스탯")]
    public int attack;
    public int defense;
    public int hp;
    public int stamina;
}
