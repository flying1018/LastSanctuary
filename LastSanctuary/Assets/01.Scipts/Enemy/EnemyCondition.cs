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
    
    //프로퍼티
    public bool IsInvincible { get; set; }
    public bool IsTakeDamageable;
    public float TakeDamageStart;
    
    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _attack = _enemy.Data.attack;
        _maxHp = _enemy.Data.hp;
        _defense = _enemy.Data.defense;
        _curHp = _maxHp;
    }
    
    private void Update()
    {
        if (IsTakeDamageable)
        {
            if ( Time.time - TakeDamageStart >= 0.5f) //임시
            {
                IsTakeDamageable = false;
            }
        }
    }

    public void TakeDamage(int atk, Transform attackDir, DamageType type)
    {
        if (IsInvincible) return;
        if (IsTakeDamageable) return;
        IsTakeDamageable = true;
        TakeDamageStart = Time.time;
        
        ApplyDamage(atk);
        if (type == DamageType.Heavy)
        {
            ChangingHitState();
        }
        else
        {
            OnHitEffected();
        }

    }

    private void OnHitEffected()
    {
        StartCoroutine(HitEffectCoroution());
    }
    private IEnumerator HitEffectCoroution()
    {
        SpriteRenderer sprite = _enemy.SpriteRenderer;
        Color Hitcolor = sprite.color;
        
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sprite.color = Hitcolor;
    }
    public void ApplyDamage(int atk)
    {
        _damage = atk;//계산식
        _curHp -= _damage;
        Debug.Log($"대미지를 받음{_damage}");
    }
    public void KnockBack(Transform dir)
    {
        throw new NotImplementedException();
    }
    public void ChangingHitState()
    {
        _enemy.StateMachine.ChangeState(_enemy.StateMachine.HitState);
    }
}
