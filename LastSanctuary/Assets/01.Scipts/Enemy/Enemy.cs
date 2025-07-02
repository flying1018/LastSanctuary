using UnityEngine;

public enum MonsterType
{
    Idle,
    Patrol,
}

public class Enemy : MonoBehaviour
{
    //필드
    private CapsuleCollider2D _capsuleCollider;

    //직렬화
    [field: SerializeField] public EnemyAnimationDB AnimationDB {get; private set;}
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private GameObject enemyModel;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float platformCheckDistance;
    [SerializeField] private MonsterType type;
    //투사체?
    
    //프로퍼티
    public EnemyStateMachine StateMachine { get; set; }
    public EnemySO Data {get => enemyData;}
    public Rigidbody2D Rigidbody {get; set;}
    public Animator Animator {get; set;}
    public SpriteRenderer SpriteRenderer { get; set; }
    public EnemyCondition Condition { get; set; }
    public Transform Target { get; set; }
    public Transform SpawnPoint { get; set; }
    public MonsterType Type { get => type; set => type = value; }
    public bool IsRight { get; set; } = true;

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<EnemyCondition>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        AnimationDB.Initailize();
        
        StateMachine = new EnemyStateMachine(this);
        
    }
    private void Update()
    {
        StateMachine.HandleInput();
        StateMachine.Update();
    }

    public bool IsPlatform()
    {
        float setX = IsRight ? 0.3f: -0.3f;
        Vector2 newPos = new Vector2(transform.position.x + setX, transform.position.y);
        Debug.DrawRay(newPos, Vector2.down * platformCheckDistance, Color.red);
        return Physics2D.Raycast(newPos, Vector2.down,
            platformCheckDistance,platformLayer);
    }

    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }

    public void Init(Transform spawnPoint)
    {
        SpawnPoint = spawnPoint;
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<EnemyCondition>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        Condition.Init(this);
        StateMachine = new EnemyStateMachine(this);
    }
}
