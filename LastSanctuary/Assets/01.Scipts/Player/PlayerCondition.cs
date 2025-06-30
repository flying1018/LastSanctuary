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

    //test용
    private float _damageReduction = 0.8f;
    private int _steminaRecovery = 30;
    private int _staminaCost = 30;

    public bool IsPerfectGuard { get; set; }
    public bool IsGuard { get; set; }
    public bool IsInvincible { get; set; }


    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void TakeDamage(int atk, DamageType type)
    {
        if (IsInvincible) { return; }

        if (IsPerfectGuard && type != DamageType.Contact)
        {
            Debug.Log("PerfectGuard");
            _curStamina += _steminaRecovery;
            return;
        }
        if (IsGuard && type != DamageType.Contact)
        {
            _totaldamage = Mathf.CeilToInt(atk * (1 - _damageReduction));
            _curStamina -= _staminaCost;
            Debug.Log($"가드 성공 받은 데미지:{_totaldamage}");
        }
        else
        {
            _totaldamage = atk;
            _curHp -= _totaldamage;
            Debug.Log($"데미지를 받음{_totaldamage}");
            if (_curHp <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }

    public void PlayerRecovery()
    {
        DebugHelper.Log($"1. {_curHp} 현재 체력");
        _curHp = _maxHp;
        _curStamina = _maxStamina;

        DebugHelper.Log($"2. {_curHp} 현재 체력");
    }

    public void ChangeInvincible(bool Invincible)
    {
        IsInvincible = Invincible;
    }
}
