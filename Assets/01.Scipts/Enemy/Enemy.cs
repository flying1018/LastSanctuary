using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStateMachine stateMachine;
    [SerializeField] private Transform taget;
    [SerializeField] public EnemySO Data;

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }

    private void Start()
    {
        stateMachine.ChangeState(new EnemyIdleState(stateMachine));
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
