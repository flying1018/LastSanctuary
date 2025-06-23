using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStateMachine stateMachine;
    [SerializeField] private Transform taget;
    [SerializeField] public EnemySO Data;

    //public CharacterController Controller {get; private set;} 쓸지안쓸지모름

    private void Awake()
    {
        stateMachine = new EnemyStateMachine(this);
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
