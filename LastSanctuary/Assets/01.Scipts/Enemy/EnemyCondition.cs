using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCondition : MonoBehaviour, IDamageable
{
    //필드
    private Enemy _enemy;
    private int _curHp;
    private int _maxHp;
    private int _attack;
    private int _defense;
    private int _damage;
    private bool _isTakeDamageable;
    //프로퍼티
    public bool IsInvincible { get; set; }

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _attack = _enemy.Data.attack;
        _maxHp = _enemy.Data.hp;
        _defense = _enemy.Data.defense;
        _curHp = _maxHp;
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
        StartCoroutine(HitEffect_Coroutine());
    }
    private IEnumerator HitEffect_Coroutine()
    {
        SpriteRenderer sprite = _enemy.SpriteRenderer;
        Color originColor = sprite.color;
        
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, _enemy.Data.alphaValue);
        yield return new WaitForSeconds(_enemy.Data.hitDuration);
        sprite.color = originColor;
    }
    private void Death()
    {
        StartCoroutine(Death_Coroutine());
    }
    private IEnumerator Death_Coroutine()
    {
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.DeathParameterHash);
        _enemy.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        yield return new WaitForSeconds(_enemy.Data.deathTime);
        ObjectPoolManager.Set(_enemy.Data._key, _enemy.EnemyModel, _enemy.gameObject);
    }

    public void TakeDamage(int atk, DamageType type, Transform attackDir, float knockBackPower)
    {
        if (IsInvincible) return;
        if (_isTakeDamageable) return;
        StartCoroutine(DamageDelay_Coroutine());
        
        ApplyDamage(atk);
        if (_curHp <= 0)
        {
            Death();
        }
        else
        {
            if (knockBackPower>0)
            {
                Debug.Log("asd");
                ChangingHitState();
                KnockBack(attackDir, knockBackPower);
            }
            else
            {
                OnHitEffected();
            }
        }
    }

    public void ApplyDamage(int atk)
    {
        _damage = atk;//계산식
        _curHp -= _damage;
        Debug.Log(_damage);
    }
    public void KnockBack(Transform dir, float force)
    {
        Debug.Log("KnockBack");
        Debug.Log(force);
        Vector2 knockbackDir = (transform.position - dir.position);
        knockbackDir.y = 0f;
        _enemy.Rigidbody.AddForce(knockbackDir.normalized * force,ForceMode2D.Impulse);
    }
    
    public void ChangingHitState()
    {
        _enemy.StateMachine.ChangeState(_enemy.StateMachine.HitState);
    }
}
