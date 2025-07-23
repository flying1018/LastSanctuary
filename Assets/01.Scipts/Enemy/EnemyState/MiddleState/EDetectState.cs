using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDetectState : EnemyBaseState
{
    public EDetectState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.IdleParameterHash);
        
        base.Enter();
        
        _time = 0;
        
        //유저 방향 확인
        Vector2 direction = _enemy.Target.position - _enemy.transform.position;
        Rotate(direction);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_enemy.AnimationDB.IdleParameterHash);
    }
}

//지상 몬스터 용
public class EnemyDetectState : EDetectState
{
    public EnemyDetectState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    
    
    public override void Update()
    {
        //공격 쿨타임 체크
        base.Update();
        
        //인식 상태가 끝나면
        _time += Time.deltaTime;
        if (_time > _data.detectTime && _enemy.Target != null)
        {   //추적
            _stateMachine.ChangeState(_stateMachine.ChaseState);
        }
    }
}
