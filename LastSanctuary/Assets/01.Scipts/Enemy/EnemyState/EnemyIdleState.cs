using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine StateMachine) : base(StateMachine)
    {
    }

    public override void Enter()
    {
        if (_enemy.Type == MonsterType.Patrol )//임시
        {
            _stateMachine.ChangeState(_stateMachine.PatrolState);
        }
        StartAnimation(_enemy.AnimationDB.IdleParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.IdleParameterHash);
    }
    
    public override void Update()
    {
        base.Update();
        if (_enemy.Target != null)
        {
            _stateMachine.ChangeState(_stateMachine.DetectState);
        }
    }
}
