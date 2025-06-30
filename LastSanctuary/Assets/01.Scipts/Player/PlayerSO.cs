using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfo
{
    public int attackIndex;
    public float multiplier;
    public GameObject effectPrefab;
    public float nextComboTime;
}

[Serializable]
public class PlayerAttackData
{
    [field:SerializeField] public List<AttackInfo> AttackInfoDatas { get; private set; }
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfo GetAttackInfo(int index) { return AttackInfoDatas[index]; }
}

[Serializable]
[CreateAssetMenu(fileName = "Player", menuName = "New Player")]
public class PlayerSO : ScriptableObject
{
    [field: Header("GroundState")]
    [field: SerializeField][field: Range(0f, 20f)] public float moveSpeed { get; private set; } = 6f;

    [field: Header("AirState")]
    [field: SerializeField][field: Range(0f, 100f)] public float jumpForce { get; private set; } = 10f;

    [Header("BattleState")]
    public int damage;
    public int hp;
    public int stamina;
    public int defense;

    [field: Header("DashState")]
    [field: SerializeField][field: Range(0f, 1.5f)] public float dashTime { get; private set; } = 0.25f;
    [field: SerializeField][field: Range(0f, 50f)] public float dashPower { get; private set; } = 20f;
    
    [field: Header("HealState")]
    [field: SerializeField][field: Range(0f, 100f)] public float HealAmount{ get; private set; } = 15f;
    [field: SerializeField][field: Range(0f, 2f)] public float HealDuration{ get; private set; } = 3f;


    [Header("AttackState")] 
    [field: SerializeField] public PlayerAttackData attacks;
}
