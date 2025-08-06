using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerCondition : IDamageable, IKnockBackable,IGuardable
{
     public void Init(Player player)
    {
        _player = player;
        MaxHp = _player.Data.hp;
        MaxStamina = _player.Data.stamina;
        _staminaRecovery = _player.Data.staminaRecovery;
        Defence = _player.Data.defense;
        Attack = _player.Data.attack;
        MaxUltimate = _player.AttackData.maxUltimateGauge;
        
        CurHp = MaxHp;
        CurStamina = MaxStamina;
        HealAmonut = _player.Data.healAmount;
        CurUltimate = 0;
        
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
    public void TakeDamage(WeaponInfo weaponInfo)
    {
        //무적 일때
        if (IsInvincible) return;
        DamageType = weaponInfo.DamageType;
        //대미지 계산
        ApplyDamage(weaponInfo.Attack, weaponInfo.Defpen);
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
        int damage = (int)Math.Ceiling(atk - TotalDefence * (1 - defpen));
        CurHp -= damage;
    }

    #endregion

    #region KnockBack
    
    //넉백 계산
    public void ApplyKnockBack(WeaponInfo weaponInfo ,Transform dir)
    {
        if(DontKnockBack) return;
        if (weaponInfo.KnockBackForce > 0)
        {
            Vector2 knockbackDir = (transform.position - dir.transform.position);
            knockbackDir.y = 0;
            Vector2 knockback = knockbackDir.normalized * weaponInfo.KnockBackForce;
            _player.Move.GravityAddForce(knockback,_player.Data.gravityPower);
        }
    }
    
    #endregion
    
    #region Guard

    public bool ApplyGuard(WeaponInfo weaponInfo,Transform dir)
    {
        bool isFront = IsFront(dir);

        if (TryPerfectGuard(isFront))
        {
            _player.EventSFX2();
            _curStamina += _player.Data.perfactGuardStemina;
            //궁극기 게이지 회복

            if (weaponInfo.Condition is BossCondition bossCondition)
            {
                //보스는 그로기 게이지 상승
                _player.WeaponInfo.GroggyDamage = _player.Data.perfactGuardGroggy;
                bossCondition.ApplyGroggy(_player.WeaponInfo);
            }
            if (weaponInfo.Condition is EnemyCondition enemyCondition && weaponInfo.DamageType != DamageType.Range)
            {
                //적은 그로기 처리
                enemyCondition.ChangeGroggyState(_player.Skill.GroggyTime);
            }

            return true;
        }
        else if (TryGuard(isFront))
        {
            _player.EventSFX3();

            weaponInfo.Attack = Mathf.CeilToInt(weaponInfo.Attack * (1 - _player.Skill.DamageReduceRate));
            ApplyDamage(weaponInfo.Attack);

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
