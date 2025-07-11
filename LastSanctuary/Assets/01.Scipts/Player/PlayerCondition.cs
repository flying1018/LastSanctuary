using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : Condition, IDamageable, IKnockBackable
{
    [SerializeField] private float deathTime = 2f;
    [SerializeField] private float respawnTime = 2f;
    [SerializeField] private float reviveInvincibleTime = 0.5f;
    
    
    private Player _player;
    private float _curStamina;
    private int _staminaRecovery;
    private Coroutine _buffCoroutine;
    private StatObjectSO _lastBuffData;
    private bool _isGuard;
    private bool _isPerfectGuard;

    
    //기본 스탯 프로퍼티
    public int MaxHp { get => _maxHp; set => _maxHp = value; }
    public float MaxStamina { get; set; }
    public int Attack { get => _attack; set => _attack = value; }
    public int Defence { get => _defence; set => _defence = value; }
    
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
    public float InvincibleStart { get; set; }
    

    private void Awake()
    {
        _player = GetComponent<Player>();
        MaxHp = _player.Data.hp;
        MaxStamina = _player.Data.stamina;
        _staminaRecovery = _player.Data.staminaRecovery;
        Defence = _player.Data.defense;
        Attack = _player.Data.attack;
        _curHp = MaxHp;
        _curStamina = MaxStamina;
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


    public void TakeDamage(int atk, DamageType type, Transform dir ,float defpen = 0f)
    {
        if (IsInvincible) return;
        
        bool isFront = IsFront(dir);
        _isPerfectGuard = TryPerfectGuard(type, isFront);
        _isGuard = TryGuard(type, isFront);
        
        if(_isPerfectGuard)return;
        if (_isGuard)
        {
            atk = Mathf.CeilToInt(atk * (1 - _player.Data.damageReduction));
            ApplyDamage(atk);
            return;
        }
        
        ApplyDamage(atk ,defpen);
        if (_curHp <= 0)
        {
            Death();
        }
        else
        {
            _player.StateMachine.ChangeState(_player.StateMachine.HitState);
        }
    }
    
    public void ApplyDamage(int atk,float defpen = 0f)
    {
        int damage = (int)Math.Ceiling(atk -  _defence * (1-defpen));
        _curHp -= damage;
    }
    

    public void ApplyKnockBack(Transform dir, float force)
    {
        if(_isPerfectGuard)return;
        if (_isGuard) return;
        
        if (force > 0)
        {
            Vector2 knockbackDir = (transform.position - dir.transform.position);
            knockbackDir.y = 0f;
            Vector2 knockback = knockbackDir.normalized * force;
            _player.Rigidbody.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    public void PlayerRecovery()
    {
        _curHp = _maxHp;
        _curStamina = MaxStamina;
    }
    
    public void Heal(int healAmount)
    {
        if (_curHp < _maxHp)
        {
            _curHp += healAmount;
            _curHp = Mathf.Clamp(_curHp, 0, _maxHp);
        }
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
        if (_curStamina < MaxStamina)
        {
            _curStamina += _staminaRecovery * Time.deltaTime;
            _curStamina = Mathf.Clamp(_curStamina, 0, MaxStamina);
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
            SoundManager.Instance.PlaySFX(_player.Data.perfectGuardSound);
            Debug.Log("perfect guard");
            _curStamina += _player.Data.perfactGuardStemina; //퍼펙트 가드시 스태미나회복
            //궁극기 게이지 회복
            //보스 그로기 상승
            return true;
        }
        return false;
    }
    //가드 확인
    private bool TryGuard(DamageType type, bool isFront)
    {
        if (IsGuard && type != DamageType.Contact && isFront && UsingStamina(_player.Data.guardCost))
        {
            SoundManager.Instance.PlaySFX(_player.Data.guardSound);
            return true;
        }
        return false;
    }
    
    public void ApplyTempBuff(StatObjectSO data)
    {
        if (_buffCoroutine != null)
        {
            StopCoroutine(_buffCoroutine);
            RemoveTempBuff(_lastBuffData);
        }

        BuffHp += data.hp;
        BuffStamina += data.stamina;
        BuffAtk += data.attack;
        BuffDef += data.defense;

        _lastBuffData = data;
        _buffCoroutine = StartCoroutine(BuffDurationTimerCoroutine(data.duration));
    }

    private void RemoveTempBuff(StatObjectSO data)
    {
        BuffHp -= data.hp;
        BuffStamina -= data.stamina;
        BuffAtk -= data.attack;
        BuffDef -= data.defense;
        //죽었을떄는 전체 초기화
    }

    private IEnumerator BuffDurationTimerCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        RemoveTempBuff(_lastBuffData);
        _buffCoroutine = null;
    }

    public float HpValue()
    {
        return (float)_curHp / MaxHp;
    }

    public float StaminaValue()
    {
        return _curStamina / MaxStamina;
    }
    
    /// <summary>
    /// 플레이어가 죽었을때 델리게이트로 호출
    /// 플레이어의 인풋막고, Rigidbody도 멈춰놓음
    /// </summary>
    public override void Death()
    {
        DebugHelper.Log("OnDie 호출됨");

        IsInvincible = true;
        _player.Animator.SetTrigger(_player.AnimationDB.DieParameterHash);
        _player.Input.enabled = false;

        _player.Rigidbody.velocity = Vector2.zero;
        // 추후 죽었을때 표기되는 UI, 연출 추가 요망 (화면 암전 등)
        StartCoroutine(Revive_Coroutine());
    }

    /// <summary>
    /// 리스폰할때 실행되는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Revive_Coroutine()
    {
        yield return new WaitForSeconds(deathTime); // 앞 애니메이션 대기

        transform.position = SaveManager.Instance.GetSavePoint();
        _player.Animator.SetTrigger(_player.AnimationDB.RespawnParameterHash);
        yield return new WaitForSeconds(respawnTime);

        PlayerRecovery();
        _player.StateMachine.ChangeState(_player.StateMachine.IdleState);
        _player.Animator.Rebind();

        _player.Input.enabled = true;
        yield return new WaitForSeconds(reviveInvincibleTime);
        IsInvincible = false;

    }

}
