using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    //필드
    public WeaponInfo WeaponInfo;
    
    //직렬화
    [field: SerializeField] public PlayerAnimationDB AnimationDB { get; private set; }
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private PlayerAttackSO playerAttackData;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask aerialPlatformLayer;

    //프로퍼티
    public BoxCollider2D BoxCollider { get; set; }
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerController Input { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public Animator Animator { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public PlayerCondition Condition { get; set; }
    public bool IsRoped { get; set; }
    public Vector2 RopedPosition { get; set; }
    public PlayerWeapon PlayerWeapon { get; set; }
    public GameObject Weapon { get; set; }
    public PlayerInventory Inventory { get; set; }
    public PlayerInput PlayerInput { get; set; }
    public PlayerKinematicMove Move { get; set; }
    public IInteractable InteractableTarget { get; set; }
    public GameObject Target { get; set; }
    public PlayerCamera Camera { get; set; }
    //직렬화 데이터 프로퍼티
    public PlayerSO Data { get => playerData; }
    public PlayerAttackSO AttackData { get => playerAttackData; }


    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        Input = GetComponent<PlayerController>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<PlayerCondition>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PlayerWeapon = GetComponentInChildren<PlayerWeapon>();
        Weapon = PlayerWeapon.gameObject;
        Inventory = GetComponent<PlayerInventory>();
        PlayerInput = GetComponent<PlayerInput>();
        Move = GetComponent<PlayerKinematicMove>();
        Camera = GetComponentInChildren<PlayerCamera>();
        
        //무기 대미지 설정
        WeaponInfo = new WeaponInfo();
        WeaponInfo.Defpen = AttackData.defpen;
        WeaponInfo.Condition = Condition;
        WeaponInfo.DamageType = DamageType.Attack;
        WeaponInfo.UltimateValue = AttackData.ultimateValue;
        
        AnimationDB.Initailize();
        Inventory.Init(this);
        Condition.Init(this);
        Camera.Init(this);
        Move.Init(BoxCollider.size.x, BoxCollider.size.y,Rigidbody);
        
        StateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        if (_eventProduction) return;   //이벤트 중에는 막기
        StateMachine.HandleInput();
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        if (_eventProduction) return;   //이벤트 중에는 막기
        StateMachine.PhysicsUpdate();
        //Debug.Log(StateMachine.currentState);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //사다리 판단
        if (other.CompareTag(StringNameSpace.Tags.Ladder))
        {
            IsRoped = true;
            RopedPosition = other.transform.position;
        }

        if ((interactableLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            if(other.TryGetComponent(out IInteractable interactable))
            {
                InteractableTarget = interactable; // PlayerController에서 Interact()을 하기위한 용도
            }
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        //사다리 나가기
        if (other.CompareTag(StringNameSpace.Tags.Ladder))
        {
            IsRoped = false;
            RopedPosition = Vector2.zero;
        }

        if ((interactableLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            InteractableTarget = null;
        }
    }



    #region Need MonoBehaviour Method

    private bool _eventProduction = false;
    public void EventProduction(bool onOff)
    {
        
        if (onOff)  //이벤트 시작
        {
            UIManager.Instance.OnOffUI(false);
            PlayerInput.enabled = false;
            _eventProduction = true;
        }
        else    //이벤트 종료
        {
            UIManager.Instance.OnOffUI(true);
            PlayerInput.enabled = true;
            _eventProduction = false;
        }
    }
    
    //주변에 플레이어 있는지 확인
    public bool FindTarget()
    {
        // 정지된 상태에서 감지
        Collider2D[] overlappingHits = Physics2D.OverlapCircleAll(transform.position, AttackData.detectionRange);

        foreach (Collider2D hit in overlappingHits)
        {
            if (hit.CompareTag(StringNameSpace.Tags.Enemy))
            {
                Target = hit.gameObject;
                return true;
            }
        }

        return false;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackData.detectionRange);
    }
    
    #endregion
    
    #region AnimationEvent Method
    public void AnimationEvent1()
    {
        if (StateMachine.currentState is PlayerBaseState playerBaseState)
        {
            playerBaseState.PlayEvent1();
        }
    }
    
    public void EventSFX1()
    {
        if (StateMachine.currentState is PlayerBaseState playerBaseState)
        {
            playerBaseState.PlaySFX1();
        }
    }

    public void EventSFX2()
    {
        if (StateMachine.currentState is PlayerBaseState playerBaseState)
        {
            playerBaseState.PlaySFX2();
        }
    }
    #endregion
}
