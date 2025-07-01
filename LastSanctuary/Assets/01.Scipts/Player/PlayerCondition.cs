using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    private Player _player;
    private int _totaldamage;
    private int _maxHp = 1000;
    private int _curHp;
    private float _maxStamina = 100;
    private float _curStamina;
    private int _staminaRecovery;
    
    public bool IsPerfectGuard { get; set; }
    public bool IsGuard { get; set; }
    public bool IsInvincible { get; set; }
    public float InvincibleStart { get; set; }
    public DamageType DamageType { get; set; }
    public event Action OnDie;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _maxHp = _player.Data.hp;
        _maxStamina = _player.Data.stamina;
        _staminaRecovery = _player.Data.staminaRecovery;
        _curHp = _maxHp;
        _curStamina = _maxStamina;
    }

    private void Update()
    {
        //무적 해제
        if (IsInvincible)
        {
            if ( Time.time - InvincibleStart >= _player.Data.invincibleDuration)
            {
                IsInvincible = false;
                Debug.Log("무적 해제");
            }
        }
        
        //스테미나 회복
        if (_curStamina < _maxStamina)
        {
            _curStamina += _staminaRecovery * Time.deltaTime;
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
            _curStamina += data.guardSteminaRecovery; //퍼펙트 가드시 스태미나회복
            //궁극기 게이지 회복
            //보스 그로기 상승
            return;
        }
        if (IsGuard && type != DamageType.Contact && isFront && UsingStamina(data.guardStaminaCost))
        {
            _totaldamage = Mathf.CeilToInt(atk * (1 - data.damageReduction));
            Debug.Log($"가드 성공 받은 데미지:{_totaldamage}");
        }
        else
        {
            DamageType = type;  //경직 시간 판단을 위한 대미지 타입
            _totaldamage = atk;
            _curHp -= _totaldamage;
            Debug.Log($"데미지를 받음{_totaldamage}");
            if (_curHp <= 0)
            {
                OnDie?.Invoke();
            }
            else
            {
                //넉백
                Vector2 knockbackDir = (transform.position - dir.transform.position);
                knockbackDir.y = 0;
                knockbackDir.Normalize();
                Vector2 knockback = knockbackDir * data.knockbackForce;
                _player.Rigidbody.AddForce(knockback, ForceMode2D.Impulse);
                
                //hit 상태로 전환
                _player.StateMachine.ChangeState(_player.StateMachine.HitState);
            }
        }
    }

    public void PlayerRecovery()
    {
        _curHp = _maxHp;
        _curStamina = _maxStamina;
    }

    public void ChangeInvincible(bool invincible)
    {
        IsInvincible = invincible;
    }
    
    public bool UsingStamina(int stamina)
    {
        if (_curStamina >= stamina)
        {
            _curStamina -= stamina;
            return true;
        }
        return false;
    }
    
}
