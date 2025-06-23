
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }
    
    public virtual void Enter()
    {
    }
    public virtual void Exit()
    {
    }
    public virtual void HandleInput()
    {
    }
    public virtual void Update()
    {
    }
    public virtual void PhysicsUpdate()
    {
    }

    protected void Move()
    {
        Vector3 moveDirection = GetMoveDiretion();
        Move(moveDirection);
    }

    private Vector3 GetMoveDiretion()
    {
        Vector3 moveDirection = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).normalized;
        return moveDirection;
    }

    private void Move(Vector3 moveDirection)
    {
        float moveSpeed = GetMoveSpeed();
        stateMachine.Enemy.transform.position += moveDirection * (moveSpeed * Time.deltaTime);
    }

    private float GetMoveSpeed()
    {
        return stateMachine.MoveSpeed;
    }

}
