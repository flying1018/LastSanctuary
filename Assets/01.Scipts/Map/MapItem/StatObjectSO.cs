using UnityEngine;


/// <summary>
/// 스탯 오브젝트 데이터
/// </summary>
[CreateAssetMenu(fileName = "StatObjectSO", menuName = "New StatObjectSO")]
public class StatObjectSO : ScriptableObject
{
    [Header("분류")]
    public string UID;
    public bool isConsumable;
    public Sprite icon;

    [Header("스탯")]
    public StatDelta[] statDeltas;
    public float duration;
    
    [Header("Sound")]
    public AudioClip getSound;
}