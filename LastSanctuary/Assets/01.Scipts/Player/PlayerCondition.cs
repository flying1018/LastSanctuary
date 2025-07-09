using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    private Player _player;
    private int _totaldamage;
    private int _curHp;
    private float _curStamina;
    private int _staminaRecovery;
    
    public int Attack { get; set; }
    public int Defence{ get; set; }
    public float MaxStamina { get; set; }
    public int MaxHp { get; set; }
    public bool IsPerfectGuard { get; set; }
    public bool IsGuard { get; set; }
    public bool IsInvincible { get; set; }
    public float InvincibleStart { get; set; }
    public DamageType DamageType { get; set; }
    public event Action OnDie;

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

    public void TakeDamage(int atk, DamageType type, Transform dir = null,float force = 0f)
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
            ChangingHitState();
            if(force>0)
                KnockBack(dir,force);
        }
    }
    
    public void PlayerRecovery()
    {
        _curHp = MaxHp;
        _curStamina = MaxStamina;
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
    private bool TryGuard(int atk, DamageType type, bool isFront)
    {
        if (IsGuard && type != DamageType.Contact && isFront && UsingStamina(_player.Data.guardCost))
        {
            SoundManager.Instance.PlaySFX(_player.Data.guardSound);
            atk = Mathf.CeilToInt(atk * (1 - _player.Data.damageReduction));
            ApplyDamage(atk);
            Debug.Log("가드 성공");
            return true;
        }
        return false;
    }

    public void ApplyDamage(int atk)
    { 
        _totaldamage = atk;//계산식
        _curHp -= _totaldamage;
        Debug.Log($"대미지를 받음{_totaldamage}");
    }
    //넉백
    public void KnockBack(Transform dir,float force)
    {
        Vector2 knockbackDir = (transform.position - dir.transform.position);
        knockbackDir.y = 0f;
        Vector2 knockback = knockbackDir.normalized * force;
        _player.Rigidbody.AddForce(knockback, ForceMode2D.Impulse);
    }
    //상태전환
    public void ChangingHitState()
    {
        _player.StateMachine.ChangeState(_player.StateMachine.HitState);
    }

    public void Heal(int healAmount)
    {
        if (_curHp < MaxHp)
        {
            _curHp += healAmount;
            _curHp = Mathf.Clamp(_curHp, 0, MaxHp);
        }
    }
    public IEnumerator ApplyTempBuffCoroutine(int hp, int stamina, int atk, int def, float duration )
    {
         MaxHp += hp;
         _curHp += hp;
         MaxStamina += stamina;
         _curStamina += stamina;
         Attack += atk;
         Defence += def;
         yield return new WaitForSeconds(duration);
         StartCoroutine(RemoveTempBuffCoroutine(hp, stamina, atk, def));

    }

    private IEnumerator RemoveTempBuffCoroutine(int hp, int stamina, int atk, int def)
    {
        MaxHp -= hp;
        _curHp -= hp;
        MaxStamina -= stamina;
        _curStamina -= stamina;
        Attack -= atk;
        Defence -= def;
        yield break;
    }
}
