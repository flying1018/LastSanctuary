using UnityEngine;

[CreateAssetMenu(fileName = "StatObjectSO", menuName = "New StatObjectSO")]
public class StatObjectSO : ScriptableObject
{
    [Header("분류")]
    public string UID;
    public bool isConsumable;
    public Sprite icon;

    [Header("스탯")]
    public int attack;
    public int defense;
    public int hp;
    public int stamina;
    public float duration;
}