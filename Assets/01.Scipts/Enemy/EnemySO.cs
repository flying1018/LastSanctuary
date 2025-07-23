using System;
using UnityEngine;

/// <summary>
/// 몬스터에 필요한 초기 데이터
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Info")]
    public int _key;
    public string _name;

    [Header("Move")]
    public float gravityPower;
    
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
    public GameObject arrowPrefab;
    public int arrowPoolId;
    public int arrowPower;

    [Header("Flying Attack")] 
    public float flyingHeight;
    public float flyingSpeed;

    [Header("Sound")] 
    public AudioClip attackSound;
    public AudioClip hitSound;

}
