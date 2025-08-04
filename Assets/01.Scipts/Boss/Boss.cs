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

    public WeaponInfo WeaponInfo;
    
    //직렬화
    [field: SerializeField] public BossAnimationDB AnimationDB {get; private set;}
    [SerializeField] private BossSO bossData;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool uiOn;

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
    public KinematicMove Move {get; set;}
    public BossItemDropper ItemDropper {get; set;}

    public bool UIOn { get => uiOn; }

    public void Awake()
    {
        //필요한 프로퍼티 설정
        AnimationDB = new BossAnimationDB(); 
        BoxCollider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Condition = GetComponent<BossCondition>();
        BossWeapon = GetComponentInChildren<BossWeapon>();
        Weapon = BossWeapon.gameObject;
        ItemDropper = GetComponent<BossItemDropper>();
        Move = GetComponent<KinematicMove>();
    }

    public void Init(BossEvent bossEvent)
    {
        BossEvent = bossEvent;
        //무기 데이터 설정
        WeaponInfo = new WeaponInfo();
        WeaponInfo.Condition = Condition;
        WeaponInfo.Attack = Data.attack;
        WeaponInfo.Defpen = Data.defpen;
        WeaponInfo.KnockBackForce = Data.knockbackForce;
        WeaponInfo.DamageType = DamageType.Attack;
        
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
                damageable.TakeDamage(WeaponInfo);
            }

            if (other.gameObject.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.ApplyKnockBack(WeaponInfo,transform);
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
