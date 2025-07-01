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

    [Header("condition")]
    public int attack;
    public int hp;
    public int defense;
    public int attackRange;
}
