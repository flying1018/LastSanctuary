using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    //필드
    private EnemyStateMachine _stateMachine;
    private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private Transform taget;
    public GameObject spawnPoint;
    //직렬화
    //[field: SerializeField] public EnemyAnimationDB AnimationDB {get; private set;}
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private GameObject enemyModel;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float platformCheckDistance;
    //투사체?
    
    //프로퍼티
    public EnemySO Data {get => enemyData;}
    public Rigidbody2D Rigidbody {get; set;}
    public Animator Animator {get; set;}
    public SpriteRenderer SpriteRenderer { get; set; }
    public EnemyCondition Condition { get; set; }
    public GameObject Model { get=> enemyModel; }
    
    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<EnemyCondition>();
        SpriteRenderer = enemyModel.GetComponent<SpriteRenderer>();
        _stateMachine = new EnemyStateMachine(this);
        
    }
    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    public bool IsPlatform()
    {
        Debug.DrawRay(transform.position, Vector2.down * platformCheckDistance, Color.red);
        return Physics2D.Raycast(transform.position, Vector2.down,
            platformCheckDistance,platformLayer);
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }
}
