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
    private int _defence;
    
    public int Damage { get; private set; }
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
        _defence = _player.Data.defense;
        Damage = _player.Data.damage;
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
    }

    public void TakeDamage(int atk, Transform dir,DamageType type)
    {
        if (IsInvincible) return;
        bool isFront = IsFront(dir);
        if(TryPerfectGuard(type, isFront))return;
        if(TryGuard(atk,type, isFront))return;
        
        DamageType = type; //경직 시간 판단을 위한 대미지 타입
        ApplyDamage(atk);
        if (_curHp <= 0)
        {
            OnDie?.Invoke();
        }
        else
        {
            KnockBack(dir);
            ChangingHitState();
        }
    }
    
    public void PlayerRecovery()
    {
        _curHp = _maxHp;
        _curStamina = _maxStamina;
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

    //스테미나 회복
    public void RecoveryStamina()
    {
        if (_curStamina < _maxStamina)
        {
            _curStamina += _staminaRecovery * Time.deltaTime;
            _curStamina = Mathf.Clamp(_curStamina, 0, _maxStamina);
        }
    }
    
    //정면 확인
    private bool IsFront(Transform dir)
    {
        bool attackFromRight = dir.position.x > transform.position.x;
        bool playerDir = !_player.SpriteRenderer.flipX;
        return attackFromRight == playerDir;
    }
    //퍼펙트가드 확인
    private bool TryPerfectGuard(DamageType type, bool isFront)
    {
        if (IsPerfectGuard && type != DamageType.Contact && isFront)
        {
            _curStamina += _player.Data.perfactGuardStemina; //퍼펙트 가드시 스태미나회복
            //궁극기 게이지 회복
            //보스 그로기 상승
            return true;
        }
        return false;
    }
    //가드 확인
    private bool TryGuard(int atk, DamageType type, bool isFront)
    {
        if (IsGuard && type != DamageType.Contact && isFront && UsingStamina(_player.Data.guardCost))
        {
            atk = Mathf.CeilToInt(atk * (1 - _player.Data.damageReduction));
            ApplyDamage(atk);
            Debug.Log("가드 성공");
            return true;
        }
        return false;
    }
    //대미지 적용
    public void ApplyDamage(int atk)
    { 
        _totaldamage = atk;//계산식
        _curHp -= _totaldamage;
        Debug.Log($"대미지를 받음{_totaldamage}");
    }
    //넉백
    public void KnockBack(Transform dir)
    {
        Vector2 knockbackDir = (transform.position - dir.transform.position);
        knockbackDir.y = 0;
        knockbackDir.Normalize();
        Vector2 knockback = knockbackDir * _player.Data.knockbackForce;
        _player.Rigidbody.AddForce(knockback, ForceMode2D.Impulse);
    }
    //상태전환
    public void ChangingHitState()
    {
        _player.StateMachine.ChangeState(_player.StateMachine.HitState);
    }
    
}
