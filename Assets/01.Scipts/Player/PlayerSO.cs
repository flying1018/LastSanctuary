using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 필요한 데이터
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "Player", menuName = "Player/New Player")]
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
    public int perfactGuardUlt;
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
    public float cameraTopView;
    public float cameraBottomView;
    public Sprite whiteBackGround;
}

