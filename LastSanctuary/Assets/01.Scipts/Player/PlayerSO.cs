using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfo
{
    public AnimationClip attackAnim;
    public float animTime => attackAnim.length;
    public int attackIndex;     //공격 번호
    public float multiplier;    //공격 배수
    public float nextComboTime; //다음 콤보까지 여유 시간
    public int staminaCost;     //스테미나 코스트
    public float attackForce;   //공격시 전진 파워
    public float knockbackForce;//넉백 파워
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
    public AudioClip moveSound;

    [field: Header("AirState")]
    [field: SerializeField][field: Range(0f, 100f)] public float jumpForce { get; private set; } = 10f;

    [field: Header("DashState")]
    [field: SerializeField][field: Range(0f, 1.5f)] public float dashTime { get; private set; } = 0.25f;
    [field: SerializeField][field: Range(0f, 50f)] public float dashPower { get; private set; } = 20f;
    [field: SerializeField][field: Range(0f, 1f)] public float dashCoolTime { get; private set; } = 0.5f;
    [field: SerializeField] public int dashCost { get; private set; } = 25;
    
    [field: Header("HealState")]
    [field: SerializeField][field: Range(0f, 100f)] public int HealAmount{ get; private set; } = 15;
    [field: SerializeField][field: Range(0f, 2f)] public float HealDuration{ get; private set; } = 3f;

    [field: Header("GuardState")] 
    [field: SerializeField] public float perfectGuardWindow { get; private set; } = 0.2f;
    [field: SerializeField] public int perfactGuardStemina { get; private set; } = 30;
    [field: SerializeField] public int guardCost { get; private set; } = 30;
    public AudioClip guardSound;
    public AudioClip perfectGuardSound;
    

    [Header("AttackState")] 
    [field: SerializeField] public PlayerAttackData attacks;

    public AudioClip attackSound;

    [field: Header("HitState")]
    [field: SerializeField] public float LightHitDuration{ get; private set; } = 0.2f;
    [field: SerializeField] public float HeavyHitDuration{ get; private set; } = 0.2f;
    [field: SerializeField] public float invincibleDuration { get; private set; } = 1f;
    [field: SerializeField] public int hitSteminaRecovery { get; private set; } = 15;
    [field: SerializeField, Range(0f, 1f)] public float damageReduction { get; private set; } = 0.8f;
    public AudioClip hitSound;

    [field: Header("Condition")]
    [field: SerializeField] public int hp;
    [field: SerializeField] public int damage;
    [field: SerializeField] public int defense;
    [field: SerializeField] public int stamina;
    [field: SerializeField] public int staminaRecovery { get; private set; } = 15;

    [Header("Inventory")]
    public int potionNum;
    public int relicNum;


}
