using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingAttack : EAttackState
{
    private Vector2 _targetDir;
    
    public EnemyFlyingAttack(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    public override void Enter()
    {
        //공격에 관련된 정보 초기화
        _time = 0;
        _stateMachine.attackCoolTime = 0;

        //애니메이션 실행
        StartAnimation(_enemy.AnimationDB.AttackParameterHash);

        //공격력 정보 넘겨주기
        _enemy.EnemyWeapon.Condition = _condition;
        _enemy.EnemyWeapon.Damage = _data.attack;
        _enemy.EnemyWeapon.knockBackForce = _data.knockbackForce;
        if (_enemy.EnemyWeapon is FlyingWeapon weapon)
        {
            weapon.Init(this);
        }
        
        _targetDir = DirectionToTarget();
    }
    
    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.AttackParameterHash);
    }

    public override void Update()
    {
        _time += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        FlyingAttack();
    }
    
    //포물선 그리면서 돌진
    private void FlyingAttack()
    {
        float x = _targetDir.x > 0 ? _data.flyingAttackDistance : -_data.flyingAttackDistance;
        float y = -_data.flyingHeight + _data.flyingHeight * _time;
        
        Vector2 direction = new Vector2(x, y);
        _move.Move(direction.normalized * _data.flyingSpeed);
    }

    public void ChangeIdleState()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }
}
