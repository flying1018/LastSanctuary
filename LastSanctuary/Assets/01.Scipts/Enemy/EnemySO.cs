using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Info")]
    public int _key;
    public string _name;

    [Header("DetectState")]
    public float detectTime;
    public float detectDistance;
    
    [Header("ChaseState")]
    public float moveSpeed;
    public float cancelChaseTime;

    [field: Header("PatrolState")]
    [field: SerializeField] public float patrolDistance { get; private set; } = 10f;

    [field: Header("HitState")]
    [field: SerializeField] public float HitDuration{ get; private set; } = 0.5f;

    [Header("attackState")] 
    public float attackDistance;
    public float attackDuration;
    public float knockbackForce;
    public AnimationClip attackAnim;
    public float AnimTime => attackAnim.length; 
    
    [Header("condition")]
    public int attack;
    public int hp;
    public int defence;
    public float damageDelay;
    public float hitDuration;
    public float alphaValue;
    public AnimationClip deathAnim;
    public float deathTime => deathAnim.length;
    
    [Header("Range Attack")]
    public GameObject arrowPrefab;
    public int arrowPoolId;
    public int arrowPower;

    [Header("Rush Attack")]
    public float rushSpeed;
    
}
