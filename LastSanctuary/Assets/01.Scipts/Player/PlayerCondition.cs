using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    private Player _player;

    public event Action OnDie;


    private int _curHp;
    private int _maxHp = 1000;
    private int _curStamina;
    private int _maxStamina = 100;
    private int _totaldamage;


    public bool IsPerfectGuard { get; set; }
    public bool IsGuard { get; set; }
    public bool IsInvincible { get; set; }
    public float InvincibleStart { get; set; }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _curHp = _maxHp;
        _curStamina = _maxStamina;
    }

    private void Update()
    {
        //무적 해제
        if (IsInvincible)
        {
            if ( Time.time - InvincibleStart >= _player.Data._invincibleDuration)
            {
                IsInvincible = false;
                Debug.Log("무적 해제");
            }
        }
    }

    public void TakeDamage(int atk, Transform dir,DamageType type)
    {
        var data = _player.Data;
        bool attackFromRight = dir.position.x > transform.position.x;
        bool playerDir = !_player.SpriteRenderer.flipX;
        bool isFront = attackFromRight == playerDir;
        
        if (IsInvincible) return;
        if (IsPerfectGuard && type != DamageType.Contact && isFront)
        {
            _curStamina += data._guardSteminaRecovery; //퍼펙트 가드시 스태미나회복
            //궁극기 게이지 회복
            //보스 그로기 상승
            return;
        }
        if (IsGuard && type != DamageType.Contact && isFront)
        {
            _totaldamage = Mathf.CeilToInt(atk * (1 - data._damageReduction));
            _curStamina -= data._guardStaminaCost; //가드시 스태미나 소모
            //스태미나 부족시 가드 실패
        }
        else
        {
            //type(약공격, 강공격)에 따라 경직 시간 변화 (HitState에서 구현해야 할지 고민중)
            _totaldamage = atk;
            _curHp -= _totaldamage;
            _curStamina += data._hitSteminaRecovery; //피격시 스태미나회복
            if (_curHp <= 0)
            {
                OnDie?.Invoke();
            }
            else
            {
                Vector2 knockback = (transform.position - dir.transform.position).normalized;

                _player.StateMachine.ChangeState(_player.StateMachine.HitState);
            }
        }
    }

    public void PlayerRecovery()
    {
        _curHp = _maxHp;
        _curStamina = _maxStamina;
    }

    public void ChangeInvincible(bool Invincible)
    {
        IsInvincible = Invincible;
    }
}
