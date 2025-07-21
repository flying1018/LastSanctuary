using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCondition : Condition, IDamageable,IKnockBackable
{
    //필드
    private Enemy _enemy;
    private Coroutine _hitEffectCoroutine;
    private Material _originMaterial;
    
    //프로퍼티
    public bool IsInvincible { get; set; }
    public bool IsDeath { get; set; }
 
    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _attack = _enemy.Data.attack;
        _maxHp = _enemy.Data.hp;
        _defence = _enemy.Data.defence;
        _curHp = _maxHp;
        _delay = _enemy.Data.damageDelay;
        _originMaterial = _enemy.SpriteRenderer.material;
        _isTakeDamageable = false;
        IsInvincible = false;
        IsDeath = false;
    }
    
    //대미지 입을 때
    public void TakeDamage(int atk, DamageType type, float defpen)
    {
        if (IsInvincible) return;
        if (_isTakeDamageable) return;
        
        ApplyDamage(atk,defpen);
        
        if (_curHp <= 0)
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.DeathState);
        }
        else
        {
            OnHitEffected();
        }
        
        //대미지 입는 사운드
        SoundManager.Instance.PlaySFX(_enemy.Data.hitSound);
    }
    
    //대미지 입는 이펙트
    private void OnHitEffected()
    {
        if (_hitEffectCoroutine != null)
        {
            StopCoroutine(_hitEffectCoroutine);
            _hitEffectCoroutine = null;       
        }
        _hitEffectCoroutine = StartCoroutine(HitEffect_Coroutine());
    }
    
    //스프라이트 흰색으로 점멸하는 코루틴
    private IEnumerator HitEffect_Coroutine()
    {
        SpriteRenderer sprite = _enemy.SpriteRenderer;
        sprite.material = _enemy.Data.hitMaterial;
        yield return new WaitForSeconds(_enemy.Data.hitDuration);
        sprite.material = _originMaterial;
    }

    //넉백 처리하는 메서드
    public void ApplyKnockBack(Transform attackDir, float knockBackPower)
    {
        if (knockBackPower <= 0) return;
        if (_curHp <= 0) return;
        
        Vector2 dir = new Vector2(transform.position.x - attackDir.position.x,0).normalized;
        Vector2 knockback = dir * knockBackPower;
        _enemy.Move.AddForce(knockback);
        _enemy.StateMachine.ChangeState(_enemy.StateMachine.HitState);
    }
    

    //대미지 계산
    public void ApplyDamage(int atk, float defpen)
    {
        int damage;
        damage = (int)Math.Ceiling(atk -  _defence * (1-defpen));
        _curHp -= damage;
    }
    
    //TestCode
    public void ChangeHitState()
    {
        _enemy.StateMachine.ChangeState(_enemy.StateMachine.HitState);
    }
}
