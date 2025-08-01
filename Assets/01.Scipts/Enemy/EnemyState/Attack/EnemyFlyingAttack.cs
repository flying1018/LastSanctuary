using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingAttack : EAttackState
{
    private Vector2 _targetDir;
    
    public Vector2 PrevDir { get; private set; }
    
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
        if (_enemy.EnemyWeapon is FlyingWeapon weapon)
        {
            weapon.Init(this);
        }
        
        _targetDir = DirectionToTarget();
        _targetDir.y = 0;
        _targetDir.Normalize();
        
        PrevDir = _targetDir;
    }
    
    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.AttackParameterHash);
    }

    public override void Update()
    {
    }

    public override void PhysicsUpdate()
    {
        FlyingAttack();
    }
    
    //포물선 그리면서 돌진
    private void FlyingAttack()
    {
        _time += Time.fixedDeltaTime;
        
        float x = _targetDir.x > 0 ? _data.attackDistance : -_data.attackDistance;
        float y = -_data.flyingHeight + _data.flyingHeight * _time;
        
        Vector2 direction = new Vector2(x, y);
        _move.Move(direction.normalized * _data.flyingSpeed);
        Rotate(direction);
    }

    public void ChangeIdleState()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }
    
    public void ResetPrevDir()
    {
        PrevDir = Vector2.zero;
    }
}
