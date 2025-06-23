using UnityEngine;

public class EnemyReturnState :EnemyBaseState
{
    public EnemyReturnState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Return Enter");
    }

    public override void Exit()
    {
        Debug.Log("Return Exit");
    }

    public override void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
