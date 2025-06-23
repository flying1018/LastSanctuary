using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private Transform enemyPos;
    private Transform targetPos;
    
    [SerializeField] private Collider2D colloder;

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

    public void ColliderEnter2D(Collider2D collider)
    {
        
    }

    public override void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
       float distance = Vector2.Distance(stateMachine.Enemy.transform.position, stateMachine.Target.transform.position);
       if (distance < 10f)
       {
           stateMachine.ChangeState(stateMachine.ChaseState);
       }
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
