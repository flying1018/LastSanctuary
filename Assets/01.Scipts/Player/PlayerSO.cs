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
    public int groggyDamage; //그로기 증가량
    public bool isInvincible; //해당 공격 시 무적여부
}


/// <summary>
/// 플레이어의 필요한 데이터
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "Player", menuName = "New Player")]
public class PlayerSO : ScriptableObject
{
    [Header("GroundState")] 
    public float moveSpeed;
    public float gravityPower;

    [Header("AirState")] 
    public float jumpForce;
    public float jumpDuping;
    public float holdJumpDuping;
    public float fallJudgment;

    [Header("DashState")] 
    public AnimationClip dashAnim;
    public float DashTime => dashAnim.length;
    public float dashPower;
    public float dashCoolTime;
    public int dashCost;
    
    [Header("HealState")]
    public int healAmount;
    public AnimationClip healAnim;
    public float HealTime => healAnim.length;


    [Header("GuardState")] 
    public float perfectGuardWindow;
    public int perfactGuardStemina;
    public int perfactGuardGroggy;
    public int guardCost;
    

    [Header("AttackState")] 
    public AttackInfo[] attacks;
    public float Defpen;
    public AttackInfo dashAttack;
    public float maxUltimateGauge;
    public AttackInfo jumpAttack;
    public AttackInfo UltAttack;

    [field: Header("HitState")]
    [field: SerializeField] public float LightHitDuration{ get; private set; } = 0.2f;
    [field: SerializeField] public float HeavyHitDuration{ get; private set; } = 0.2f;
    [field: SerializeField] public float invincibleDuration { get; private set; } = 1f;
    [field: SerializeField] public int hitSteminaRecovery { get; private set; } = 15;
    [field: SerializeField, Range(0f, 1f)] public float damageReduction { get; private set; } = 0.8f;
    
    
    [Header("DeathState")]
    public float deathTime;
    
    [Header("RespawnState")]
    public float respawnTime;
    
    [Header("InteractState")]
    public AnimationClip interactionAnim;
    public float InteractionTime => interactionAnim.length;

    [Header("Condition")]
    public int hp;
    public int attack;
    public int defense;
    public int stamina;
    public int staminaRecovery;

    [Header("Inventory")]
    public int potionNum;
    
    [Header("Sounds")]
    public AudioClip moveSound;
    public AudioClip dashSound;
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip guardSound;
    public AudioClip perfectGuardSound;
    public AudioClip ropeSound;
    public AudioClip deathSound;
    public AudioClip arrowHitSound;
    public AudioClip hitSound;
    public AudioClip healSound;
    
    
    public AudioClip teleportStartSound;
    public AudioClip teleportEndSound;


}
