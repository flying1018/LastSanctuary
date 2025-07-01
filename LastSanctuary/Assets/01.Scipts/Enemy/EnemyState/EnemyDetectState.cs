using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectState : EnemyBaseState
{
    private float _time;
    public EnemyDetectState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //애니메이션 시작
        
        //유저 방향 확인
        _time = 0;
        Vector2 direction = _enemy.Target.position - _enemy.transform.position;
        Rotate(direction);
    }

    public override void Exit()
    {
        base.Exit();
        //애니메이션 종료
    }
    
    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time > _data.detectTime && _enemy.Target != null)
        {
            _stateMachine.ChangeState(_stateMachine.ChaseState);
        }
    }
}
