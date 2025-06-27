using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour,IDamageable
{
    private Player _player;
    private StateMachine _stateMachine;
    
    private int _curHp;
    private int _maxHp;
    private int _curStemina;
    private int _maxStemina;
    private int _totaldamage;

    public bool IsPerfectGuard {get; set;}
    public bool IsGuard {get; set;}
  

    private void Awake()
    {
        _player = GetComponent<Player>();
        _stateMachine = GetComponent<StateMachine>();
    }
    
    public void TakeDamage(int atk, DamageType type)
    {
        if (IsPerfectGuard && type != DamageType.Contact)
        {
            Debug.Log("PerfectGuard");
            return;
        }
        if (IsGuard && type != DamageType.Contact)
        {
            _totaldamage = Mathf.CeilToInt(atk * 0.2f);
            Debug.Log($"가드 성공 받은 데미지:{_totaldamage}");
        }
        else
        {
            _totaldamage = atk;
            _curHp -= _totaldamage;
            Debug.Log($"데미지를 받음{_totaldamage}");
            if (_curHp <= _totaldamage)
            {
                //Die
            }
        }
    }
}
