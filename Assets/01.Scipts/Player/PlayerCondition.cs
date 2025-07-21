using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : Condition, IDamageable, IKnockBackable,IGuardable
{
    private Player _player;
    private float _curStamina;
    private int _staminaRecovery;

    private Dictionary<StatObjectSO, Coroutine> _tempBuffs = new();

    //기본 스탯 프로퍼티
    public int MaxHp { get => _maxHp; set => _maxHp = value; }
    public float MaxStamina { get; set; }
    public int Attack { get => _attack; set => _attack = value; }
    public int Defence { get => _defence; set => _defence = value; }

    //성물로 증가 가능한 프로퍼티
    public int HealAmonut { get; set; }
    public float MaxUltimateGauge { get; set; }

    //버프 표시 프로퍼티
    public int BuffHp { get; set; }
    public float BuffStamina { get; set; }
    public int BuffAtk { get; set; }
    public int BuffDef { get; set; }

    //가드 프로퍼티
    public DamageType DamageType { get; set; }
    public bool IsGuard { get; set; }
    public bool IsPerfectGuard { get; set; }

    //무적 처리 프로퍼티
    public bool IsInvincible { get; set; }


    public void Init(Player player)
    {
        _player = player;
        MaxHp = _player.Data.hp;
        MaxStamina = _player.Data.stamina;
        _staminaRecovery = _player.Data.staminaRecovery;
        Defence = _player.Data.defense;
        Attack = _player.Data.attack;
        _curHp = MaxHp;
        _curStamina = MaxStamina;
        HealAmonut = _player.Data.healAmount;
        MaxUltimateGauge = _player.Data.maxUltimateGauge;
        IsInvincible = false;
    }

    public void InvincibleFunc(float time)
    {
        StartCoroutine(Invincible_Coroutine(time));
    }
    
    public IEnumerator Invincible_Coroutine(float time)
    {
        IsInvincible = true;
        yield return new WaitForSeconds(time);
        IsInvincible = false;
    }

    #region Damage
    
    //대미지 처리
    public void TakeDamage(int atk, DamageType type, float defpen = 0f)
    {
        //무적 일때
        if (IsInvincible) return;
        DamageType = type;
        //대미지 계산
        ApplyDamage(atk, defpen);
        if (_curHp <= 0)
        {
            _player.StateMachine.ChangeState(_player.StateMachine.DeathState);
        }
        else
        {
            _player.StateMachine.ChangeState(_player.StateMachine.HitState);
        }
    }

    //대미지 계산
    public void ApplyDamage(int atk, float defpen = 0f)
    {
        int damage = (int)Math.Ceiling(atk - _defence * (1 - defpen));
        _curHp -= damage;
    }
    
    #endregion

    //넉백 계산
    public void ApplyKnockBack(Transform dir, float force)
    {
        if (force > 0)
        {
            Vector2 knockbackDir = (transform.position - dir.transform.position);
            Vector2 knockback = knockbackDir.normalized * force;
            _player.Move.AddForce(knockback);
        }
    }

    #region Condition
    
    //플레이어 회복
    public void PlayerRecovery()
    {
        _curHp = _maxHp;
        _curStamina = MaxStamina;
    }

    //플레이어 체력 회복
    public void Heal()
    {
        if (_curHp < _maxHp)
        {
            _curHp += HealAmonut;
            _curHp = Mathf.Clamp(_curHp, 0, _maxHp);
        }
    }

    //스테미나 사용
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
        if (_curStamina < MaxStamina)
        {
            _curStamina += _staminaRecovery * Time.deltaTime;
            _curStamina = Mathf.Clamp(_curStamina, 0, MaxStamina);
        }
    }
    #endregion

    #region Buff
    
    //버프 적용
    public void ApplyTempBuff(StatObjectSO data)
    {
        if (_tempBuffs.TryGetValue(data, out Coroutine lastCoroutine))
        {
            StopCoroutine(lastCoroutine);
            RemoveTempBuff(data);
            _tempBuffs.Remove(data);
        }

        BuffHp += data.hp;
        BuffStamina += data.stamina;
        BuffAtk += data.attack;
        BuffDef += data.defense;

        Coroutine newCoroutine = StartCoroutine(BuffDurationTimerCoroutine(data));
        _tempBuffs[data] = newCoroutine;
    }

    //버프 제거
    private void RemoveTempBuff(StatObjectSO data)
    {
        BuffHp -= data.hp;
        BuffStamina -= data.stamina;
        BuffAtk -= data.attack;
        BuffDef -= data.defense;
        //죽었을떄는 전체 초기화
    }

    //버프 지속시간 타이머
    private IEnumerator BuffDurationTimerCoroutine(StatObjectSO data)
    {
        yield return new WaitForSeconds(data.duration);
        RemoveTempBuff(data);
        _tempBuffs.Remove(data);
    }
    #endregion

    #region UI
    
    //UI에 사용되는 hp
    public float HpValue()
    {
        return (float)_curHp / MaxHp;
    }

    //UI가 사용되는 스태미나
    public float StaminaValue()
    {
        return _curStamina / MaxStamina;
    }

    #endregion

    #region Guard
    
    public bool ApplyGuard(int atk, Condition condition, Transform dir, DamageType type)
    {
        bool isFront = IsFront(dir);
        
        if (TryPerfectGuard(isFront))
        {
            _player.EventSFX2();
            _curStamina += _player.Data.perfactGuardStemina; 
            //궁극기 게이지 회복
            
            if (condition is BossCondition bossCondition)
            {
                //보스는 그로기 게이지 상승
                bossCondition.ApplyGroggy(_player.Data.perfactGuardGroggy);
            }

            if (condition is EnemyCondition enemyCondition && type != DamageType.Range)
            {
                //적은 그로기 처리
                //그로기 상태 미구현
                enemyCondition.ChangeHitState();
            }
            
            return true;
        }
        else if (TryGuard(isFront))
        {
            _player.EventSFX1();
            
            atk = Mathf.CeilToInt(atk * (1 - _player.Data.damageReduction));
            ApplyDamage(atk);
            
            if (_curHp <= 0)
            {
                _player.StateMachine.ChangeState(_player.StateMachine.DeathState);
            }
            return true;
        }
        else
        {
            return false;
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
    private bool TryPerfectGuard(bool isFront)
    {
        return IsPerfectGuard && isFront;
    }
    
    //가드 확인
    private bool TryGuard(bool isFront)
    {
        return IsGuard && isFront && UsingStamina(_player.Data.guardCost);
    }
    #endregion
}
