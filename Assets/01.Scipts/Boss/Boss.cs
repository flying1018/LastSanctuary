using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //필드

    //직렬화
    [field: SerializeField] public BossAnimationDB AnimationDB {get; private set;}
    [SerializeField] private BossSO bossData;

    //프로퍼티
    public BossEvent BossEvent { get; set; }
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
    public bool Phase2 { get; set; }

    public void Init(BossEvent bossEvent)
    {
        //필요한 프로퍼티 설정
        BossEvent = bossEvent;
        AnimationDB = new BossAnimationDB(); 
        PolygonCollider = GetComponent<PolygonCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Condition = GetComponent<BossCondition>();
        BossWeapon = GetComponentInChildren<BossWeapon>();
        Weapon = BossWeapon.gameObject;
        
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

    //통제로 개선 필요
    //각각 상태마다 사용하는 애니메이션 이벤트가 다르기 때문에
    //메서드는 하나만을 사용하고 분기는 상태로 하는것으로 하고 싶음.
    
    //Attack2에서 사용하는 애니메이션 이벤트
    public void BackJump()
    {   
        Vector2 backDir = transform.position - Target.position;
        backDir.y = Mathf.Abs(backDir.x);
        Rigidbody.AddForce(backDir.normalized * Data.backJumpPower, ForceMode2D.Impulse);
        
        //플레이어가 가까이 붙었을 때 점프를 안하는 버그가 있음.
    }

    //점프 멈추기
    public void StopMove()
    {
        Rigidbody.velocity = Vector2.zero;
    }
    
    //투사체 날리기
    public void Attack()
    {
        if (StateMachine.currentState is BossAttackState attack2)
            attack2.FireProjectile();
    }
    
    //Attack3에서 사용하는 이벤트
    private Coroutine _chasePlayerCoroutine;
    
    //플레이어에게 추적
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

    //추적 정지
    public void StopChasePlayer()
    {
        if (_chasePlayerCoroutine != null)
        {
            StopCoroutine(_chasePlayerCoroutine);
            _chasePlayerCoroutine = null;
        }
    }

    //중력이 필요 없을 때
    public void KinematicOn()
    {
        PolygonCollider.isTrigger = true;
        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    //중력이 필요 할 때
    public void KinematicOff()
    {
        PolygonCollider.isTrigger = false;
        Rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
    
    //연출용 애니메이션 이벤트
    
    //카메라 흔들기
    public void LandingCameraShake()
    {
        BossEvent.CameraShake();
    }
    public void HowlingCameraShake()
    {
        BossEvent.CameraShake(Data.howlingSound.length/2); 
    }
    
    //효과음 실행르 위한 메서드
    public void PlaySFX1()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            if (bossBaseState.SoundClip == null) return;
            SoundManager.Instance.PlaySFX(bossBaseState.SoundClip[0]);

        }
    }

    public void PlaySFX2()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            if (bossBaseState.SoundClip == null) return;
            SoundManager.Instance.PlaySFX(bossBaseState.SoundClip[1]);
        }
    }   
    
    public void PlaySFX3()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            if (bossBaseState.SoundClip == null) return;
            SoundManager.Instance.PlaySFX(bossBaseState.SoundClip[2]);
        }
    }    
    
    public void PlaySFX4()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            if (bossBaseState.SoundClip == null) return;
            SoundManager.Instance.PlaySFX(bossBaseState.SoundClip[3]);
        }
    }

    #endregion
    
}
