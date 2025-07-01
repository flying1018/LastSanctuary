using TMPro;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine StateMachine) : base(StateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle Enter");
    }

    public override void Exit()
    {
        Debug.Log("Idle Exit");
    }
    
    public override void Update()
    {
        if (_enemy.Target != null)
        {
            _stateMachine.ChangeState(_stateMachine.ChaseState);
        }
    }
}
