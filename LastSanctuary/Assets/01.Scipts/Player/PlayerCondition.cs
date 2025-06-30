using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    private Player _player;

    public event Action OnDie;
    
    
    private int _curHp;
    private int _maxHp;
    private int _curStamina;
    private int _maxStamina;
    private int _totaldamage;
    

    public bool IsPerfectGuard {get; set;}
    public bool IsGuard {get; set;}
  

    private void Awake()
    {
        _player = GetComponent<Player>();
    }
    
    public void TakeDamage(int atk, Transform dir,DamageType type)
    {
        var data = _player.Data;

        bool attackFromRight = dir.position.x > transform.position.x;
        bool playerDir = !_player.SpriteRenderer.flipX;
        bool isFront = attackFromRight == playerDir;
        
        if (IsPerfectGuard && type != DamageType.Contact && isFront)
        {
            Debug.Log("PerfectGuard");
            _curStamina += data._guardSteminaRecovery;
            return;
        }
        if (IsGuard && type != DamageType.Contact && isFront)
        {
            _totaldamage = Mathf.CeilToInt(atk * (1 - data._damageReduction));
            _curStamina -= data._guardStaminaCost;
            Debug.Log($"가드 성공 받은 데미지:{_totaldamage}");
        }
        else
        {
            _totaldamage = atk;
            _curHp -= _totaldamage;
            Debug.Log($"데미지를 받음{_totaldamage}");
            if (_curHp <= _totaldamage)
            {
                OnDie?.Invoke();
            }
            else
            {
                
            }
        }
    }
}
