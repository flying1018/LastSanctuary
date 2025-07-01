using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine StateMachine) : base(StateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }
    
    public override void Update()
    {
        if (_enemy.Target != null)
        {
            _stateMachine.ChangeState(_stateMachine.DetectState);
        }
    }
}
