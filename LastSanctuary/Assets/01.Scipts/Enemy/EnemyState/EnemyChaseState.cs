using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Chase Enter");
    }

    public override void Exit()
    {
        Debug.Log("Chase Exit");
    }

    public override void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void PhysicsUpdate()
    {
        Move();
    }
}
