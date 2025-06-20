using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSO : ScriptableObject
{
    [Header("GroundState")] 
    public float moveSpeed;
    
    [Header("AirState")]
    public float jumpForce;
    
    [Header("BattleState")]
    public int damage;
    public int hp;
    public int defense;
}
