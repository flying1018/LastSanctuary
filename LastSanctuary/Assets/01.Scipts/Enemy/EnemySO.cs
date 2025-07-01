using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Info")]
    public int _key;
    public string _name;

    [Header("GroundState")]
    public float _moveSpeed;
    public float _areaRange;
    public bool isGround;
    [field: SerializeField] public float patrolDistance { get; private set; } = 10f;

    [Header("BattleState")]
    public int _attack;
    public int _hp;
    public int _defense;
    public int _attackRange;
}
