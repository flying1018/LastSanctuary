using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCondition : MonoBehaviour, IDamageable
{
    //필드
    private Enemy _enemy;
    private int _curHp;
    private int _maxHp;
    private int _attack;
    private int _defense;
    
    //프로퍼티
    public bool IsInvincible { get; set; }
    
    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _attack = _enemy.Data.attack;
        _maxHp = _enemy.Data.hp;
        _defense = _enemy.Data.defense;
        _curHp = _maxHp;
    }

    public void TakeDamage(int atk, Transform attackDir, DamageType type)
    {
        if (IsInvincible) return;
    }
}
