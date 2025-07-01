using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Info")]
    public int _key;
    public string _name;

    [Header("ChaseState")]
    public float moveSpeed;

    [Header("BattleState")]
    public int _attack;
    public int _hp;
    public int _defense;
    public int _attackRange;
}
