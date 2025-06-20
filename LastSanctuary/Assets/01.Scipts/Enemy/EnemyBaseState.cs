
public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    
    public EnemyBaseState(EnemyStateMachine EnemyStateMachine)
    {
        stateMachine = EnemyStateMachine;
    }
    
    public virtual void Enter()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Exit()
    {
        throw new System.NotImplementedException();
    }

    public virtual void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }

    public virtual void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
