using UnityEngine;

public class EIdleState : EnemyBaseState
{
    public EIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    //대기 상태
    public override void Update()
    {
        base.Update();
        
        //인식 조건
        //1. 플레이어가 인식범위 안에 있을 때
        if (_enemy.FindTarget())
        {   //인식 상태
            _stateMachine.ChangeState(_stateMachine.DetectState);
        }
    }
    
    
}

public class EnemyIdleState : EIdleState
{
    public EnemyIdleState(EnemyStateMachine StateMachine) : base(StateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.IdleParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.IdleParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        //대기 상태일 때 그 위치에서 대기
        switch (_enemy.MoveType)
        {
            case MoveType.Walk:
                Move(Vector2.zero);
                break;
            case MoveType.Fly:
                Fly(Vector2.zero);
                break;
        }
    }
}
