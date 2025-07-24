using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //필드
    private Vector3 _spawnPoint;
    //직렬화
    [field: SerializeField] public BossAnimationDB AnimationDB {get; private set;}
    [SerializeField] private BossSO bossData;
    [SerializeField] private LayerMask groundLayer;
    

    //프로퍼티
    public BossEvent BossEvent { get; set; }
    public BossStateMachine StateMachine { get; set; }
    public Rigidbody2D Rigidbody {get; set;}
    public Animator Animator {get; set;}
    public SpriteRenderer SpriteRenderer { get; set; }
    public BoxCollider2D BoxCollider {get; set;}
    public BossCondition Condition { get; set; }
    public Transform Target { get; set; }
    public BossWeapon BossWeapon { get; set; }
    public GameObject Weapon { get; set; }
    public BossSO Data {get => bossData;}
    public bool Phase2 { get; set; }
    public float VerticalVelocity { get; set;}
    public KinematicMove Move {get; set;}
    public BossItemDropper ItemDropper {get; set;}

    public void Init(BossEvent bossEvent)
    {
        //필요한 프로퍼티 설정
        BossEvent = bossEvent;
        AnimationDB = new BossAnimationDB(); 
        BoxCollider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Condition = GetComponent<BossCondition>();
        BossWeapon = GetComponentInChildren<BossWeapon>();
        Weapon = BossWeapon.gameObject;
        ItemDropper = GetComponent<BossItemDropper>();
        //transform.position = _spawnPoint;
        Move = GetComponent<KinematicMove>();
        
        Move.Init(BoxCollider.bounds.size.x, BoxCollider.bounds.size.y, Rigidbody);
        AnimationDB.Initailize(); 
        Condition.Init(this);
        Phase2 = false;
        
        StateMachine = new BossStateMachine(this);
    }



    private void OnEnable()
    {
        //오브젝트 활성화 시 플레이어 찾기
        Target = FindObjectOfType<Player>().transform;
    }
    
    private void Update()
    {
        StateMachine.HandleInput();
        StateMachine.Update();
    }
    
    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
//        Debug.Log(StateMachine.currentState);

    }

    //보스와 충돌시 넉백과 대미지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Data.attack,DamageType.Attack);
            }

            if (other.gameObject.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.ApplyKnockBack(transform,Data.knockbackForce);
            }
        }
    }

    #region need MonoBehaviour Method
    

    
    #endregion
    
    #region AnimationEvent Method
    
    //애니메이션 이벤트
    public void AnimationEvent1()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlayEvent1();
        }
    }
    
    //애니메이션 이벤트
    public void AnimationEvent2()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlayEvent2();
        }
    }
    
    
    //효과음 실행르 위한 메서드
    //사운드 실행 애니메이션 이벤트
    public void EventSFX1()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlaySFX1();
        }
    }

    public void EventSFX2()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlaySFX2();
        }
    }
    
    public void EventSFX3()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlaySFX3();
        }
    }

    #endregion
    
}
