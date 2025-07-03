using System;
using UnityEditorInternal;
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
    public Rigidbody2D Rigidbody {get; set;}
    public Animator Animator {get; set;}
    public SpriteRenderer SpriteRenderer { get; set; }
    public EnemyCondition Condition { get; set; }
    public Transform Target { get; set; }
    public Transform SpawnPoint { get; set; }
    public EnemyWeapon EnemyWeapon { get; set; }
    public bool IsRight { get; set; } = true;
    public EnemySO Data {get => enemyData;}
    
    private void Update()
    {
        StateMachine.HandleInput();
        StateMachine.Update();
    }


    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Data.attack,transform,DamageType.Contact);
            }
        }
    }

    public void Init(Transform spawnPoint)
    {
        SpawnPoint = spawnPoint;
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<EnemyCondition>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        EnemyWeapon = GetComponentInChildren<EnemyWeapon>();
        Condition.Init(this);
        AnimationDB.Initailize();
        
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
}
