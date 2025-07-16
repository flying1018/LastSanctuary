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
    [SerializeField] private float platformCheckDistance;
    [SerializeField] private IdleType idleType;
    [SerializeField] private MoveType moveType;
    [SerializeField] private AttackType attackType;
    [SerializeField] private float patrolDistance = 5;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float gravityScale = 9.8f;
   
    
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
    public float VerticalVelocity { get; set; }
    

    private void Update()
    {
        StateMachine.HandleInput();
        StateMachine.Update();
    }
    
    private void FixedUpdate()
    {
        if (Condition.IsDeath) return;

        ApplyGravity();
        StateMachine.PhysicsUpdate();
        //Debug.Log(StateMachine.currentState);
        //Debug.Log(StateMachine.attackCoolTime);
    }

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
            rushAttack.KnuckBackMe(other.gameObject.transform,Data.knockbackForce);
        }
    }

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
            rushAttack.KnuckBackMe(other.gameObject.transform,Data.knockbackForce);
        }
    }


    #region  Need MonoBehaviour Method
    public void Attack()
    {
        if (StateMachine.currentState is EnemyRangeAttackState rangeState)
            rangeState.FireArrow();
        if (StateMachine.currentState is EnemyRushAttack rushAttack)
            rushAttack.RushAttack();
    }
    
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

    private void ApplyGravity()
    {
        if(moveType == MoveType.Fly) return;
        if (IsGrounded())
        {
            VerticalVelocity = 0f;
        }
        else
        {
            VerticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        
    }

    public bool IsPlatform()
    {
        float capsulsize = CapsuleCollider.size.x / 2;
        float setX = SpriteRenderer.flipX ? -capsulsize : capsulsize;
        Vector2 newPos = new Vector2(transform.position.x + setX, transform.position.y);
        Debug.DrawRay(newPos, Vector2.down * platformCheckDistance, Color.red);
        return Physics2D.Raycast(newPos, Vector2.down,
            platformCheckDistance, platformLayer);
    }

    public bool IsGrounded()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y);
        Ray ray = new Ray(newPos, Vector2.down);
        Debug.DrawRay(ray.origin,ray.direction * CapsuleCollider.size.y / 2, Color.red);
        return Physics2D.Raycast(ray.origin,ray.direction,CapsuleCollider.size.y / 2,platformLayer);
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

        Target = null;
        return false;
    }
    
    public void PlaySFX1()
    {
        if (StateMachine.currentState is PlayerBaseState playerBaseState)
        {
            if (playerBaseState.SoundClip == null) return;
            SoundManager.Instance.PlaySFX(playerBaseState.SoundClip[0]);

        }
    }
    
    #endregion
}
        
