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
    private int _curStemina;
    private int _maxStemina;
    private int _totaldamage;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void TakeDamage(int atk, DamageType type)
    {
        if ( type != DamageType.Contact)
        {
            Debug.Log("PerfectGuard");
            return;
        }
        if ( type != DamageType.Contact)
        {
            _totaldamage = Mathf.CeilToInt(atk * 0.2f);
            Debug.Log($"가드 성공");
        }
        else
        {
            _curHp -= _totaldamage;
            Debug.Log($"데미지를 받음{_totaldamage}");
            if (_curHp <= _totaldamage)
            {
                OnDie?.Invoke();
            }
        }
    }
}
