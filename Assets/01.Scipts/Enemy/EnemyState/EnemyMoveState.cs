using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState :IState
{
    private EnemyStateMachine stateMachine;

    public EnemyMoveState(EnemyStateMachine stateMachine)
    {
        stateMachine = stateMachine;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    void IState.Update()
    {
        Update();
    }

    public void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        
    }
}
