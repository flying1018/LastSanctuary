using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    //직렬화
    [field: SerializeField] public PlayerAnimationDB AnimationDB { get; private set; }
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask aerialPlatformLayer;

    //프로퍼티
    public BoxCollider2D CapsuleCollider;
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerController Input { get; set; }
    public PlayerHandler Handler { get; set; }
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
    //직렬화 데이터 프로퍼티
    public PlayerSO Data { get => playerData; }


    private void Awake()
    {
        CapsuleCollider = GetComponent<BoxCollider2D>();
        Input = GetComponent<PlayerController>();
        Handler = GetComponent<PlayerHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Condition = GetComponent<PlayerCondition>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PlayerWeapon = GetComponentInChildren<PlayerWeapon>();
        Weapon = PlayerWeapon.gameObject;
        Inventory = GetComponent<PlayerInventory>();
        PlayerInput = GetComponent<PlayerInput>();
        
        AnimationDB.Initailize();
        Inventory.Init(this);
        
        StateMachine = new PlayerStateMachine(this);
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

    public Vector2 gravityScale = Vector2.zero;
    public Vector2 GroundDirection { get; set; }
    public Vector2 WallDirection { get; set; }
    public bool IsWall { get; set; }
    public bool IsGrounded { get; set; }
    public bool IsAerialPlatform { get; set; }
    
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
            IInteractable interactable = other.GetComponent<IInteractable>();

            if (interactable == null)
            {
                DebugHelper.LogWarning("IInteractable 컴포넌트 안달려있음");
                return;
            }

            Input.InteractableTarget = interactable; // PlayerController에서 Interact()을 하기위한 용도
            Input.IsNearInteractable = true;

            /*
            현재 IsSavePoint는 사용하지 않지만 
            추후 저장에 사용할 수 있으니 보류
            */
            if (other.CompareTag(StringNameSpace.Tags.SavePoint))
            {
                Input.IsSavePoint = true;
            }
        }
        
        //바닥 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Ground))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            GroundDirection = point - transform.position;
            
            Vector2 position = transform.position;
            position.y = point.y + (GroundDirection.y < 0 ? CapsuleCollider.size.y / 2 : -CapsuleCollider.size.y/2);
            transform.position = position;
            
            IsGrounded = true;
        }
        
        //공중 발판 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.AerialPlatform))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            GroundDirection = point - (transform.position - new Vector3(0,CapsuleCollider.size.y / 3));

            if (GroundDirection.y > 0) return; 
            
            Vector2 position = transform.position;
            position.y = point.y + CapsuleCollider.size.y / 2;
            transform.position = position;
            
            IsGrounded = true;
            IsAerialPlatform = true;
        }
        
        //벽과 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Wall))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            WallDirection = point - transform.position;

            Vector2 position = transform.position;
            position.x = point.x + (WallDirection.x < 0 ? CapsuleCollider.size.x / 2 : -CapsuleCollider.size.x/2);
            transform.position = position;
            
            IsWall = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //땅과 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Ground))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            GroundDirection = point - transform.position;
            
            Vector2 position = transform.position;
            position.y = point.y + (GroundDirection.y < 0 ? CapsuleCollider.size.y / 2 : -CapsuleCollider.size.y/2);
            transform.position = position;
        }
        
        
        //벽과 충돌 시
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Wall))
        {
            Vector3 point = other.ClosestPoint(transform.position);
            WallDirection = point - transform.position;

            Vector2 position = transform.position;
            position.x = point.x + (WallDirection.x < 0 ? CapsuleCollider.size.x / 2 : -CapsuleCollider.size.x/2);
            transform.position = position;
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

        if (other.gameObject.layer == interactableLayer)
        {
            Input.InteractableTarget = null;
            Input.IsNearInteractable = false;

            //세이브 포인트 나가기
            if (other.CompareTag(StringNameSpace.Tags.SavePoint))
            {
                Input.IsSavePoint = false;
            }
        }
        
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Ground))
        {
            IsGrounded = false;
        }
        
        if (other.gameObject.CompareTag(StringNameSpace.Tags.AerialPlatform))
        {
            IsGrounded = false;
            IsAerialPlatform = false;
        }
        
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Wall))
        {
            IsWall = false;
        }
    }



    #region Need MonoBehaviour Method

    public bool IsGround()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y - CapsuleCollider.size.y / 2);
        Ray ray = new Ray(newPos, Vector3.down);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, CapsuleCollider.size.y / 2, groundLayer);
        return hit;
    }
    
    #endregion
    
    #region AnimationEvent Method
    public void ApplyAttackForce()
    {
        AttackInfo attackInfo = null;
        if (StateMachine.currentState is PlayerAttackState attackState)
        {
            attackInfo = attackState.attackInfo;
        }
        Vector2 direction = SpriteRenderer.flipX ? Vector2.left : Vector2.right;
        Rigidbody.AddForce(direction * attackInfo.attackForce, ForceMode2D.Impulse);
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
