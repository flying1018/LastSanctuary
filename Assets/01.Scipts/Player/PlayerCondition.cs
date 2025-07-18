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
    private bool _isGuard;
    private bool _isPerfectGuard;

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
        HealAmonut = _player.Data.healAmount;
        MaxUltimateGauge = _player.Data.maxUltimateGauge;
        IsInvincible = false;
    }

    private void Update()
    {
        //무적 해제
        if (IsInvincible)
        {
            if (Time.time - InvincibleStart >= _player.Data.invincibleDuration)
            {
                IsInvincible = false;
                Debug.Log("무적 해제");
            }
        }
    }

    //대미지 처리
    public void TakeDamage(int atk, DamageType type, Transform dir, float defpen = 0f)
    {
        //무적 일때
        if (IsInvincible) return;
        
        //가드 처리
        bool isFront = IsFront(dir);
        _isPerfectGuard = TryPerfectGuard(type, isFront);
        _isGuard = TryGuard(type, isFront);

        if (_isPerfectGuard) return;
        if (_isGuard)
        {
            atk = Mathf.CeilToInt(atk * (1 - _player.Data.damageReduction));
            ApplyDamage(atk);
            return;
        }

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

    //넉백 계산
    public void ApplyKnockBack(Transform dir, float force)
    {
        if (_isPerfectGuard) return;
        if (_isGuard) return;
        
        if (force > 0)
        {
            Vector2 knockbackDir = (transform.position - dir.transform.position);
            Vector2 knockback = knockbackDir.normalized * force;
            _player.Move.AddForce(knockback);
        }
    }

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
            _player.EventSFX2();
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
            _player.EventSFX1();
            return true;
        }

        return false;
    }

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
}