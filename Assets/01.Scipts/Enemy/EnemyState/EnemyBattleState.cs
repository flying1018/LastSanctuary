using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleState : EnemyBaseState
{
    public EnemyBattleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        //공중 몹이면 하늘을 나는 애니메이션
        if(_enemy.MoveType == MoveType.Fly)
            StartAnimation(_enemy.AnimationDB.WalkParameterHash);
        //지상 몸이면 대기 애니메이션
        else if(_enemy.MoveType == MoveType.Walk)
            StartAnimation(_enemy.AnimationDB.IdleParameterHash);
    }

    public override void Exit()
    {
        //공중 몹이면 하늘을 나는 애니메이션
        if(_enemy.MoveType == MoveType.Fly)
            StopAnimation(_enemy.AnimationDB.WalkParameterHash);
        //지상 몸이면 대기 애니메이션
        else if(_enemy.MoveType == MoveType.Walk)
            StopAnimation(_enemy.AnimationDB.IdleParameterHash);
    }
    
    public override void Update()
    {
        //공격 쿨타임 체크
        base.Update();
        
        //인식 조건
        //1. 플레이어가 인식범위 안에 있을 때
        if (_enemy.FindTarget() && !WithinAttackDistnace())
        {
            _stateMachine.ChangeState(_stateMachine.ChaseState);
        }
        
        //공격 조건
        //1. 플레이어가 사정거리 안에 있을 때
        //2. 공격 쿨타임이 다 찾을 때
        if ( WithinAttackDistnace() &&
             _stateMachine.attackCoolTime > _data.attackDuration)
        {
            _stateMachine.ChangeState(_stateMachine.AttackState);
        }
    }
}
