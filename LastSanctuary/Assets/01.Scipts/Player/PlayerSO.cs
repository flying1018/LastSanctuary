using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Player", menuName = "New Player")]
public class PlayerSO : ScriptableObject
{
    [field: Header("GroundState")]
    [field: SerializeField][field: Range(0f, 20f)] public float moveSpeed { get; private set; } = 10f;

    [field: Header("AirState")]
    [field: SerializeField][field: Range(0f, 25f)] public float jumpForce { get; private set; } = 5f;

    [Header("BattleState")]
    public int damage;
    public int hp;
    public int defense;
    
    [field: Header("DashState")]
    [field: SerializeField][field: Range(0f, 50f)] public float dashSpeed { get; private set; } = 20f;
}
