using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateState : PlayerAttackState
{
    private int _hitCount = 20;
    private float _interval = 0.2f;
    private float _range = 12f;
    private float _width = 2.5f;

    private GameObject _laserObj;          // 레이저 이펙트 오브젝트
    private Collider2D _laserCollider;     // 레이저 판정용 Collider
    private WeaponInfo _ultWeaponInfo;
    public UltimateState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo)
    {
        _ultWeaponInfo = new WeaponInfo
        {
            Condition = _condition, // 플레이어 컨디션
            Attack = (int)(_condition.TotalAttack * AttackInfo.multiplier),
            KnockBackForce = AttackInfo.knockbackForce,
            GroggyDamage = AttackInfo.groggyDamage,
            Defpen = _attackData.ultDefpen,
            DamageType = DamageType.Attack,
            UltimateValue = _attackData.ultimateValue,
        };
    }

    public override void Enter()
    {
        base.Enter();
        _condition.CurUltimate = 0f;
        _player.Animator.SetTrigger("Ultimate");

        _laserObj = Object.Instantiate(_attackData.laserPrefab);
        _laserObj.transform.position = _player.transform.position +
            (_player.SpriteRenderer.flipX ? Vector3.left : Vector3.right) * _range / 2f;

        _laserObj.transform.rotation =
        Quaternion.Euler(0f, 0f,
        _player.SpriteRenderer.flipX ? -90f : 90f);

        _laserObj.transform.localScale = Vector3.one;

        _laserCollider = _laserObj.GetComponent<Collider2D>();
        _laserCollider.enabled = true;

        _player.StartCoroutine(UltimateSkill_Coroutine());

    }

    public override void Exit()
    {
        if (_laserObj != null)
        {
            _laserCollider.enabled = false;
            Object.Destroy(_laserObj);
        }
    }


    public override void HandleInput()
    {

    }

    public override void Update()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    IEnumerator UltimateSkill_Coroutine()
    {
        List<Enemy> hitEnemies = new List<Enemy>();

        for (int i = 0; i < _hitCount; i++)
        {
            Vector2 center = _laserObj.transform.position;
            Vector2 size = new Vector2(_range, _width);
            float angle = _player.SpriteRenderer.flipX ? 180f : 0f;

            Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, angle, LayerMask.GetMask("Enemy"));

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Enemy>(out var enemy))
                {
                    if (!hitEnemies.Contains(enemy))
                        hitEnemies.Add(enemy);

                    enemy.Condition.TakeDamage(_ultWeaponInfo);

                    if (enemy.Condition is EnemyCondition bossCond)
                        bossCond.ChangeGroggyState(1);
                }
            }
            yield return new WaitForSeconds(_interval);
        }

        // 궁극기 종료 후 상태 Idle 등으로
        _stateMachine.ChangeState(_stateMachine.IdleState);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
