using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//돌진 몬스터 용
public class EnemyFlyingDetectState : EDetectState
{
    private EnemyFlyingAttack _flyingAttack;
    public EnemyFlyingDetectState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    
    public override void Enter()
    {
        base.Enter();

        if (_stateMachine.AttackState is EnemyFlyingAttack enemyFlyingAttack)
        {
            _flyingAttack = enemyFlyingAttack;
        }
    }

    public override void Update()
    {
        base.Update();
        
        //인식 상태가 끝나면
        _time += Time.deltaTime;
        if (_time > _data.detectTime)
        {
            Vector2 targetDir = DirectionToTarget();
            targetDir.y = 0;
            if (_flyingAttack.PrevDir == targetDir.normalized)
                _stateMachine.ChangeState(_stateMachine.IdleState);
            else
                _stateMachine.ChangeState(_stateMachine.AttackState);
        }
    }
}
