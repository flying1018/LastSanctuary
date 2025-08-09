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
        
        CurStamina = MaxStamina;
        HealAmonut = _player.Data.healAmount;
        CurHp = MaxHp - HealAmonut;
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
    
    private Coroutine _timeSlower;
    private Coroutine _changeBackGround;
    private Coroutine _zoomInOut;

    public bool ApplyGuard(WeaponInfo weaponInfo,Transform dir)
    {
        bool isFront = IsFront(dir);

        if (TryPerfectGuard(isFront))
        {
            _player.EventSFX2();
            _curStamina += _player.Data.perfactGuardStemina;
            CurUltimate += _player.Data.perfactGuardUlt;
            
            //연출 (몰라 급해 하드코딩임)
            //UIManager.Instance.BorderFadeOut(Color.white, 0.05f);
            //_player.Camera.ShakeCamera(6,6,0.1f);
            GuardCoroutine();

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

    //일단은 하드코딩 시간 부족
    private void GuardCoroutine()
    {
        if (_zoomInOut != null)
        {
            StopCoroutine(_zoomInOut);
            _zoomInOut = null;
        }
        _zoomInOut = StartCoroutine(ZoomInOut_Coroutine(_player.Camera.transform, 4f, 0.2f));
        
        if (_timeSlower != null)
        {
            StopCoroutine(_timeSlower);
            _timeSlower = null;
        }
        _timeSlower = StartCoroutine(TimeSlower_Coroutine(0.1f, 1,0.2f));

        if (_changeBackGround != null)
        {
            StopCoroutine(_changeBackGround);
            _changeBackGround = null;
        }
        _changeBackGround = StartCoroutine(ChangeBackGround_Coroutine(0.1f));
    }

    IEnumerator ZoomInOut_Coroutine(Transform target, float zoom, float stopTime)
    {
        _player.Camera.CutZoomIn(target,zoom);
        yield return new WaitForSecondsRealtime(stopTime);
        _player.Camera.EndZoomCamera();
    }

    IEnumerator TimeSlower_Coroutine(float force,float time,float stopTime)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(stopTime);
        Time.timeScale = force;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }

    IEnumerator ChangeBackGround_Coroutine(float time)
    {
        _player.BackGround.sprite = _player.Data.whiteBackGround;
        _player.BackGround.sortingOrder = 75;
        yield return new WaitForSecondsRealtime(time);
        _player.BackGround.sprite = _player.OriginBackGround;
        _player.BackGround.sortingOrder = 0;
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
