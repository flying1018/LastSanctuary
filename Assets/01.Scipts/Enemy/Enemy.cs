using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum IdleType
{
    Idle,
    Patrol,
}

public enum MoveType
{
    Walk,
    Fly,
}

public enum AttackType
{
    Melee,
    Range,
    Rush,
}

public class Enemy : MonoBehaviour
{
    //필드
   

    //직렬화
    [field: SerializeField] public EnemyAnimationDB AnimationDB {get; private set;}
    [SerializeField] private EnemySO enemyData;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float platformCheckDistance;
    [SerializeField] private IdleType idleType;
    [SerializeField] private MoveType moveType;
    [SerializeField] private AttackType attackType;
    [SerializeField] private float patrolDistance = 5;
    
    //프로퍼티
    public CapsuleCollider2D CapsuleCollider { get; set; }
    public EnemyStateMachine StateMachine { get; set; }
    public Rigidbody2D Rigidbody {get; set;}
    public Animator Animator {get; set;}
    public SpriteRenderer SpriteRenderer { get; set; }
    public EnemyCondition Condition { get; set; }
    public Transform Target { get; set; }
    public Transform SpawnPoint { get; set; }
    public EnemyWeapon EnemyWeapon { get; set; }
    public GameObject Weapon { get; set; }
    public EnemySO Data {get => enemyData;}
    public IdleType IdleType {get => idleType;}
    public MoveType MoveType {get => moveType;}
    public AttackType AttackType {get => attackType;}
    public float PatrolDistance { get; set; }

    //생성 시
    public void Init(Transform spawnPoint, float distance)
    {
        SpawnPoint = spawnPoint;
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<EnemyCondition>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        EnemyWeapon = GetComponentInChildren<EnemyWeapon>();
        Weapon = EnemyWeapon.gameObject;
        AnimationDB.Initailize();
        PatrolDistance = distance;
        CapsuleCollider.enabled = true;
        
        Condition.Init(this);
        StateMachine = new EnemyStateMachine(this);
    }
    
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

    //공중몹 충돌 시 넉백과 대미지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                if(moveType == MoveType.Fly)
                    damageable.TakeDamage(Data.attack,DamageType.Attack,transform);
            }

            if (other.gameObject.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.ApplyKnockBack(transform,Data.knockbackForce);
            }
        }

        if (StateMachine.currentState is EnemyRushAttack rushAttack)
        {
            rushAttack.RushKnuckBack(other.gameObject.transform,Data.knockbackForce);
        }
    }
    
    
    //몬스터와 충돌시 넉백과 대미지
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                if(moveType == MoveType.Fly)
                    damageable.TakeDamage(Data.attack,DamageType.Attack,transform);
                else
                    damageable.TakeDamage(Data.attack,DamageType.Contact,transform);
            }

            if (other.gameObject.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.ApplyKnockBack(transform,Data.knockbackForce);
            }
        }

        if (StateMachine.currentState is EnemyRushAttack rushAttack)
        {
            rushAttack.RushKnuckBack(other.gameObject.transform,Data.knockbackForce);
        }
    }


    #region  Need MonoBehaviour Method
    
    //애니메이션 이벤트에서 사용하는 메서드
    public void Attack()
    {
        if (StateMachine.currentState is EnemyRangeAttackState rangeState)
            rangeState.FireArrow();
        if (StateMachine.currentState is EnemyRushAttack rushAttack)
            rushAttack.RushAttack();
    }
    
    //이동 방향에 발판이 있는지 체크
    public bool IsPlatform()
    {
        float capsulsize = CapsuleCollider.size.x / 2;
        float setX = SpriteRenderer.flipX ? -capsulsize : capsulsize;
        
        Vector2 newPos = new Vector2(transform.position.x + setX, transform.position.y);

        return Physics2D.Raycast(newPos, Vector2.down,
            platformCheckDistance, platformLayer);
    }

    //키네마틱 화 진행되면 필요 없는 코드
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

    //주변에 플레이어 있는지 확인
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

        Target = null;
        return false;
    }
    
    //사운드 실행 애니메이션 이벤트
    public void EventSFX1()
    {
        if (StateMachine.currentState is EnemyBaseState enemyBaseState)
        {
            enemyBaseState.PlaySFX1();
        }
    }
    
    #endregion
}
        
