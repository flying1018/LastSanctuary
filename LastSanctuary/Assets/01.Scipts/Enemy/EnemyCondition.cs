using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCondition : MonoBehaviour, IKnockBackable
{
    //필드
    private Enemy _enemy;
    private int _curHp;
    private int _maxHp;
    private int _attack;
    private int _defense;
    private int _damage;
    private bool _isTakeDamageable;
    private Coroutine _hitEffectCoroutine;
    private Color _originColor;
    //프로퍼티
    public bool IsInvincible { get; set; }

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _attack = _enemy.Data.attack;
        _maxHp = _enemy.Data.hp;
        _defense = _enemy.Data.defense;
        _curHp = _maxHp;
        _originColor = _enemy.SpriteRenderer.color;
        _isTakeDamageable = false;
        IsInvincible = false;
    }

    private IEnumerator DamageDelay_Coroutine()
    {
        _isTakeDamageable = true;
        yield return new WaitForSeconds(_enemy.Data.damageDelay);
        _isTakeDamageable = false;
    }
    private void OnHitEffected()
    {
        if (_hitEffectCoroutine != null)
        {
            StopCoroutine(_hitEffectCoroutine);
            _hitEffectCoroutine = null;       
        }
        _hitEffectCoroutine = StartCoroutine(HitEffect_Coroutine());
    }
    
    private IEnumerator HitEffect_Coroutine()
    {
        SpriteRenderer sprite = _enemy.SpriteRenderer;
        
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, _enemy.Data.alphaValue);
        yield return new WaitForSeconds(_enemy.Data.hitDuration);
        sprite.color = _originColor;
    }
    private void Death()
    {
        StartCoroutine(Death_Coroutine());
    }
    private IEnumerator Death_Coroutine()
    {
        _enemy.Rigidbody.velocity = Vector2.zero;
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.DeathParameterHash);
        _enemy.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        yield return new WaitForSeconds(_enemy.Data.deathTime);
        ObjectPoolManager.Set(_enemy.Data._key, _enemy.gameObject, _enemy.gameObject);
    }

    public void TakeDamage(int atk, DamageType type, Transform attackDir, float defpen)
    {
        if (IsInvincible) return;
        if (_isTakeDamageable) return;
        
        ApplyDamage(atk,defpen);
        if (_curHp <= 0)
        {
            Death();
        }
        else
        {
            ChangingState();
        }
    }

    public void ApplyKnockBack(Transform attackDir, float knockBackPower)
    {
        Debug.Log("KnockBack");
        Vector2 knockbackDir = (transform.position - attackDir.position);
        knockbackDir.y = 0f;
        _enemy.Rigidbody.AddForce(knockbackDir.normalized *  knockBackPower,ForceMode2D.Impulse);
        StartCoroutine(DamageDelay_Coroutine());
    }

    public void ApplyDamage(int atk, float defpen)
    {
        _damage = (int)Math.Ceiling(atk -  _defense * (1-defpen));
        _curHp -= _damage;
    }
  
    
    public void ChangingState()
    {
        _enemy.StateMachine.ChangeState(_enemy.StateMachine.HitState);
    }
}
