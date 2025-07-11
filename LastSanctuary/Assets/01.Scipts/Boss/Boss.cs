using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //필드

    //직렬화
    [field: SerializeField] public BossAnimationDB AnimationDB {get; private set;}
    [SerializeField] private BossSO bossData;

    //프로퍼티
    public BossStateMachine StateMachine { get; set; }
    public Rigidbody2D Rigidbody {get; set;}
    public Animator Animator {get; set;}
    public SpriteRenderer SpriteRenderer { get; set; }
    public PolygonCollider2D PolygonCollider {get; set;}
    public BossCondition Condition { get; set; }
    public Transform Target { get; set; }
    public BossWeapon BossWeapon { get; set; }
    public GameObject Weapon { get; set; }
    public BossSO Data {get => bossData;}
    public bool HasDetectedTarget { get; private set; } = false;
    public bool Phase2 { get; set; }

    private void Init()
    {
        AnimationDB = new BossAnimationDB(); 
        AnimationDB.Initailize(); 
        PolygonCollider = GetComponent<PolygonCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Condition = GetComponent<BossCondition>();
        BossWeapon = GetComponentInChildren<BossWeapon>();
        Weapon = BossWeapon.gameObject;
        
        Condition.Init(this);
        StateMachine = new BossStateMachine(this);
    }
    

    private void OnEnable()
    {
        Target = FindObjectOfType<Player>().transform;
        Init();
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

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //충돌 복구 조건
        //1. 플레이어 일것
        //2. 3공격 패턴은 예외
        if(other.CompareTag(StringNameSpace.Tags.Player) && 
           StateMachine.currentState != StateMachine.Attack3)
            KinematicOff();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //충돌 복구 조건
        //1. 플레이어 일것
        //2. 3공격 패턴은 예외
        if(other.CompareTag(StringNameSpace.Tags.Player) && 
           StateMachine.currentState != StateMachine.Attack3)
            KinematicOff();
    }


    #region need MonoBehaviour Method

    //Attack2
    public void BackJump()
    {
        Vector2 backDir = transform.position - Target.position;
        backDir.y = Mathf.Abs(backDir.x);
        Rigidbody.AddForce(backDir.normalized * Data.backJumpPower, ForceMode2D.Impulse);
    }

    public void StopMove()
    {
        Rigidbody.velocity = Vector2.zero;
    }
    
    //Attack3
    private Coroutine _chasePlayerCoroutine;
    public void ChasePlayer()
    {
        if (_chasePlayerCoroutine != null)
        {
            StopCoroutine(_chasePlayerCoroutine);
            _chasePlayerCoroutine = null;
        }
        _chasePlayerCoroutine = StartCoroutine(ChasePlayer_Coroutine());
    }

    IEnumerator ChasePlayer_Coroutine()
    {
        while (true)
        {
            Vector2 targetX= Target.position;
            targetX.y = transform.position.y;
            transform.position = targetX;
            yield return null;
        }
    }

    public void StopChasePlayer()
    {
        if (_chasePlayerCoroutine != null)
        {
            StopCoroutine(_chasePlayerCoroutine);
            _chasePlayerCoroutine = null;
        }
    }

    public void KinematicOn()
    {
        PolygonCollider.isTrigger = true;
        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public void KinematicOff()
    {
        PolygonCollider.isTrigger = false;
        Rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    #endregion
    
    public void DetectPlayer(Transform target)
    {
        if (HasDetectedTarget) return;
    
        HasDetectedTarget = true;
        Target = target;
    }
}
