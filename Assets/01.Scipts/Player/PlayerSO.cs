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

    [Header("JumpState")] 
    public float inputTimeLimit;
    public float jumpForce;
    public float jumpDuping;
    public float holdJumpDuping;
    
    [Header("FallState")]
    public float fallJudgment;

    [Header("DownJumpState")] 
    public float downJumpTime;

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
    
    
    [Header("HitState")]
    public float lightHitDuration;
    public float heavyHitDuration;
    public float invincibleDuration;
    public int hitStaminaRecovery;
    public float damageReduction;
    
    
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
    
    
    [Header("Material")]
    public Material defaultMaterial;
    public Material transparentMaterial;

    [Header("PlayerCamera")] 
    public float cameraDiff;
}


[Serializable]
[CreateAssetMenu(fileName = "PlayerAttack", menuName = "New PlayerAttack")]
public class PlayerAttackSO : ScriptableObject
{
    [Header("AttackState")] 
    public AttackInfo[] attacks;
    public float defpen;
    public float maxUltimateGauge;
    
    [Header("DashAttackState")] 
    public AttackInfo dashAttack;
    
    [Header("JumpAttackState")] 
    public AttackInfo jumpAttack;
    

    [Header("TopAttackState")] 
    public AttackInfo topAttack;

    [Header("GroggAttackState")] 
    public GameObject groggyAttackPrefab;
    public int prefabId;
    public float groggyAnimInterval;
    public AttackInfo groggyAttack;
    public float groggyTime;
    public float detectionRange;
    public float hidingTime;
    public float groggyAttackInterval;

    [Header("UltimateAttackState")]
    public AttackInfo UltAttack;
    public float ultimateValue;
}

