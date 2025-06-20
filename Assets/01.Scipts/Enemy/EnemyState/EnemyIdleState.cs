using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    protected EnemyStateMachine stateMachine;
    private Transform enemyPos;
    private Transform targetPos;
    
    [SerializeField] private Collider2D colloder;

    public EnemyIdleState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
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

    public void ColliderEnter2D(Collider2D collider)
    {
        
    }

    public override void HandleInput()
    {
        
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
