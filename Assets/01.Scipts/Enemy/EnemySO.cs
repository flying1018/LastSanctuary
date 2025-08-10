using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 몬스터에 필요한 초기 데이터
/// </summary>
public enum RangeType
{
 Traget,
 Beeline,
}
[Serializable]
[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Info")]
    public int _key;
    public string _name;

    [Header("Move")]
    public float gravityPower;
    public float runSpeed;
    
    [Header("DetectState")]
    public float detectTime;
    public float detectDistance;
    
    [Header("ChaseState")]
    public float moveSpeed;
    public float cancelChaseTime;
    
    [Header("Material")]
    public Material hitMaterial;

    [Header("attackState")] 
    public float attackDistance;
    public float attackDuration;
    public float knockbackForce;
    public AnimationClip attackAnim;
    public float AttackTime => attackAnim.length;

    [Header("GroggyState")]
    public float blinkLength;
    
    [Header("condition")]
    public int attack;
    public int hp;
    public int defence;
    
    [Header("HitState")]
    public float damageDelay;
    public float hitDuration;

    [Header("DeathState")] 
    public int dropGold;
    public AnimationClip deathAnim;
    public float DeathTime => deathAnim.length;
    
    [Header("Range Attack")]
    public GameObject projectilePrefab;
    public int projectilePower;
    public RangeType rangeType;
    public float firePointX; 

    [Header("Flying Attack")] 
    public float flyingHeight;
    public float flyingSpeed;

    [Header("Sound")] 
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip abilitySound;
    public AudioClip deathSound;
}
