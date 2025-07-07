using UnityEngine;

public enum IdleType
{
    Idle,
    Patrol,
}

public enum AttackType
{
    Melee,
    Range,
}

public enum MoveType
{
    Walk,
    Fly,
}

public class Enemy : MonoBehaviour
{
    //필드
    private CapsuleCollider2D _capsuleCollider;

    //직렬화
    [field: SerializeField] public EnemyAnimationDB AnimationDB {get; private set;}
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float platformCheckDistance;
    [SerializeField] private IdleType idleType;
    [SerializeField] private AttackType attackType;
    [SerializeField] private MoveType moveType;

    //프로퍼티
    public EnemyStateMachine StateMachine { get; set; }
    public Rigidbody2D Rigidbody {get; set;}
    public Animator Animator {get; set;}
    public SpriteRenderer SpriteRenderer { get; set; }
    public EnemyCondition Condition { get; set; }
    public Transform Target { get; set; }
    public Transform SpawnPoint { get; set; }
    public EnemyWeapon EnemyWeapon { get; set; }
    public bool IsRight { get; set; } = true;
    public GameObject Weapon { get; set; }
    public EnemySO Data {get => enemyData;}
    public IdleType IdleType {get => idleType;}
    public AttackType AttackType {get => attackType;}
    public MoveType MoveType {get => moveType;}
    
    private void Update()
    {
        StateMachine.HandleInput();
        StateMachine.Update();
    }


    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
        //Debug.Log(StateMachine.currentState);
        //Debug.Log(StateMachine.attackCoolTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Data.attack,DamageType.Contact,transform,Data.knockbackForce);
            }
        }
    }

    public void FireArrow()
    {
        if (StateMachine.AttackState is EnemyRangeAttackState rangeState)
        {
            rangeState.FireArrow();
        }

    }

    #region  Need MonoBehaviour Method
    
    public void Init(Transform spawnPoint)
    {
        SpawnPoint = spawnPoint;
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<EnemyCondition>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        EnemyWeapon = GetComponentInChildren<EnemyWeapon>();
        Weapon = EnemyWeapon.gameObject;
        AnimationDB.Initailize();
        
        Condition.Init(this);
        StateMachine = new EnemyStateMachine(this);
    }
    
    public bool IsPlatform()
    {
        float setX = IsRight ? 0.3f: -0.3f;
        Vector2 newPos = new Vector2(transform.position.x + setX, transform.position.y);
        Debug.DrawRay(newPos, Vector2.down * platformCheckDistance, Color.red);
        return Physics2D.Raycast(newPos, Vector2.down,
            platformCheckDistance,platformLayer);
    }

    public void SetCollisionEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public bool FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, Data.detectDistance, Vector2.down);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag(StringNameSpace.Tags.Player))
            {
                Target = hit.transform;
                return true;
            }
        }
        return false;
    }
    
    #endregion
}
        
