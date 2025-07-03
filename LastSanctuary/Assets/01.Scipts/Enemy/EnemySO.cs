using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Info")]
    public int _key;
    public string _name;

    [Header("DetectState")]
    public float detectTime;
    
    [Header("ChaseState")]
    public float moveSpeed;
    public float cancelChaseTime;

    [field: Header("PatrolState")]
    [field: SerializeField] public float patrolDistance { get; private set; } = 10f;

    [field: Header("HitState")]
    [field: SerializeField] public float HitDuration{ get; private set; } = 0.5f;
    
    [Header("condition-Stat")]
    public int attack;
    public int hp;
    public int defense;
    public int attackRange;
    [Header("condition-Value")]
    public float damageDelay;
    public float hitDuration;
    public float alphaValue;
    public AnimationClip deathAnim;
    public float deathTime => deathAnim.length;
}
