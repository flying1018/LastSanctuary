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
    public CollectType collectType;

    [Header("Potion")]
    public bool isMaxIncrease;
    public int healAmount;
    
    [Header("Relic")]
    public int index;
    public int attack;
    public int defense;
    public int hp;
    public int stamina;
}
