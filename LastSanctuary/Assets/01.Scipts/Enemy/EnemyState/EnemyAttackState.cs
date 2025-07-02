using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    
    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.AttackParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.AttackParameterHash);
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
        throw new System.NotImplementedException();
    }
    
}
